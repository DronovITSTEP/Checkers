using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ЭКЗ_по_Form.MainWindow;

namespace ЭКЗ_по_Form
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Button[,] buttons = null;
        public Board Board { get; set; }
        private Button selectedButton = null;
        public MainWindow()
        {
            InitializeComponent();
            Board = new Board();

            this.Closing += MainWindow_Closing; //событие закрытия окна (экстренного)         
        }
        //Запуск с 2 игроками

        // для продолжения игры
        private void ContinueSavedGame_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("gamestate.txt"))
            {
                LoadGameState();
                StartGameContinue();
            }
            else
            {
                MessageBox.Show("Нет сохранённой игры для продолжения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // выход
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // класс для обработки сообщений и ролучении сведений о приложении
        }

        private void StartTwoPlayers_Click(object sender, RoutedEventArgs e)
        {           
            StartGame(new Player(), new Player());
        }
        private void StartWithComputer_Click(object sender, RoutedEventArgs e)
        {
            StartGame(new Player(), new Computer());    
        }

        //Обработчик события нажатия на кнопку (клетку доски).
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            // Проверка на снятие выбора шашки
            if (selectedButton == clickedButton)
            {
                // Снятие выделения
                clickedButton.Background = (row + col) % 2 == 0 ? Brushes.White : Brushes.CadetBlue; // Возврат исходного цвета
                return; // Завершение метода
            }

            selectedButton = clickedButton;

            if (clickedButton.Content == null)
            {
                if (board[row, col].Player != Player.Empty && (board[row, col].Player == (isPlayer1Turn ? Player.Player1 : Player.Player2)))
                {
                    //board[row, col].Player != Player.Empty:  условие проверяет, что  действующий игрок на данной позиции в массиве board не пустой, значит ячейка уже занята игроком.
                    //(board[row, col].Player == (isPlayer1Turn ? Player.Player1 : Player.Player2))) оператор для определения, является ли текущий игрок тем игроком, который находится на позиции row, col.
                    //Указание, что ячейка выбрана:
                    selectedRow = row;
                    selectedCol = col;
                    clickedButton.Background = Brushes.Gray; // Подсветка выбранной шашки
                }
            }
            else
            {
                if (MakeMove((int)selectedRow, (int)selectedCol, row, col))
                {
                    clickedButton.Content = buttons[selectedRow.Value, selectedCol.Value].Content;
                    // строка копирует содержимое кнопки, которая находится на позиции [selectedRow.Value, selectedCol.Value] в  buttons, и присваивает его Content для  кнопки, на которую был произведен клик (clickedButton).
                    //  фигура,  находившаяся в выделенной ячейке(по координатам selectedRow и selectedCol), ljl;yf переместится на новую выбранную позицию 
                    buttons[selectedRow.Value, selectedCol.Value].Content = null; // содержимое предыдущей ячейки становится нулём для очистки
                    clickedButton.Background = (row + col) % 2 == 0 ? Brushes.White : Brushes.CadetBlue; // Вернуть исходный цвет
                    isPlayer1Turn = !isPlayer1Turn; // Смена хода
                    selectedRow = null;
                    selectedCol = null;

                    if (isAgainstComputer && !isPlayer1Turn)
                    {
                        ComputerMove();
                    }
                }
                else
                {
                    MessageBox.Show("Неверный ход!");
                }
            }
        }

        private void AddPossibleMoves(int row, int col, List<Tuple<int, int, int, int>> possibleMoves)// добавление возможных ходов
        {
            // игрок, находящийся на позиции board[row, col], равен Player.Player2, то direction устанавливается в 1, движется  вниз, в ином случае- вверх (в зависимости от игрока)
            int direction = (board[row, col].Player == Player.Player2) ? 1 : -1;
            // Движение влево
            //метод CheckMove,  проверяет возможность выполнения хода из ячейки (row, col) в новую позицию, определяемую (row + direction, col - 1).
            // фигура будет перемещаться на одну строку вниз или вверх(в зависимости от значения direction) и на один столбец влево.
            CheckMove(row, col, row + direction, col - 1, possibleMoves);
            // Движение вправо
            //перемещает фигуру на одну строку вниз или вверх и на один столбец вправо.
            CheckMove(row, col, row + direction, col + 1, possibleMoves);
            // Возможность поедания
            CheckCaptureMoves(row, col, possibleMoves);
        }

        //замена шашки на дамку (изменение обозначнеия
        private void UpdateButtonContent(int x, int y, Checker checker)
        {
            if (checker.Player == Player.Player1)
            {
                // Если шашка белая и  дамка, - WQ
                buttons[x, y].Content = checker.Status == CheckerStatus.King ? "WQ" : "W";
            }
            else if (checker.Player == Player.Player2)
            {
                // Если шашка черная и  дамка, - BQ,
                buttons[x, y].Content = checker.Status == CheckerStatus.King ? "BQ" : "B";
            }
            else
            {
                // Если шашка отсутствует- пустое поле
                buttons[x, y].Content = null;
            }
        }

        private void StartGame(IPlayer player1, IPlayer player2)
        {
            GameBoard.Children.Clear(); // Очистка предыдущего игрового поля для новой игры
            buttons = Board.GetButtons(player1, player2);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    buttons[i, j].Click += Button_Click; // Обработчик кликов
                    GameBoard.Children.Add(buttons[i, j]); // Добавление кнопок на доску
                }
            }
        }

        private void ShowStatsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowStatistics();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите сохранить текущую игру?", "Сохранить игру", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SaveGameState();
                MessageBox.Show("Состояние игры сохранено.");
            }
            else if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}