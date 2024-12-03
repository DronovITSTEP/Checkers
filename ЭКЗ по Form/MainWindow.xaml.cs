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
        private IPlayer player1, player2, currentPlayer, enemyPlayer;
        public MainWindow()
        {
            InitializeComponent();
            Board = new Board();

            this.Closing += MainWindow_Closing; //событие закрытия окна (экстренного)         
        }

        // сброс доски, создание кнопок, создание шашек первого и второго игроков
        private void StartGame()
        {
            GameBoard.Children.Clear();
            Board.CreateBoard(player1, player2);
            buttons = Board.GetButtons();

            foreach(var button : buttons) {
                button.Click += Button_Click; // Обработчик кликов
                GameBoard.Children.Add(button); // Добавление кнопок на доску
            }

            currentPlayer = player1;
            enemyPlayer = player2;
        }

        private void StartTwoPlayers_Click(object sender, RoutedEventArgs e)
        {           
            player1 = new Player();
            player2 = new Player();
            StartGame();
        }
        private void StartWithComputer_Click(object sender, RoutedEventArgs e)
        {
            player1 = new Player();
            player2 = new Computer();
            StartGame();    
        }

        //Обработчик события нажатия на кнопку (клетку доски).
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // не является ли текущий игрок компьютером
            if (!(currentPlayer is Computer)) {
                // выбор шашки для хода
                if (selectedButton == null) {
                    selectedButton = sender as Button;
                    return;
                }

                // снятие выбора шашки
                if (selectedButton == (sender as Button)){
                    selectedButton = null;
                    return;
                }

                Button currentButton = sender as Button;

                if (currentButton.Content != null && selectedButton != null) {
                    MessageBox.Show("Неверный ход");
                    return;
                }

                // получаем координаты кнопки
                int index = GameBoard.Children.IndexOf(currentButton);
                int row = index / 8;
                int col = index % 8;
                Point point = new Point(row, col);

                // ход текущего игрока
                currentPlayer.Move(enemyPlayer, selectedButton.Content as IChecker, point);

                // если еще съесть шашку нельзя, то ход передается следующему игроку
                if (!Logic.IsPossibleCapture()){
                    currentPlayer = player2;
                    enemyPlayer = player1;
                    selectedButton = null;
                }
            }
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
    
        // выход
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}