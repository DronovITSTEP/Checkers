using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ЭКЗ_по_Form.MainWindow;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using static ЭКЗ_по_Form.Player;

namespace ЭКЗ_по_Form
{
    public class Board
    {
        private const int SIZE = 8; //размер доски (8x8 клеток).
        private Button[,] buttons = null;
        public Board()
        {
            buttons = new Button[SIZE,SIZE];
        }

        public void CreateBoard(IPlayer player1, IPlayer player2)
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    buttons[row, col] = new Button();
                    buttons[row, col].Background = (row + col) % 2 == 0 ? Brushes.White : Brushes.Red; // Чередование цветов клеток
                    buttons[row, col].Margin = new Thickness(0);
                    buttons[row, col].Height = 50;
                    buttons[row, col].Width = 50;

                    if (row < 3 && (row + col) % 2 != 0) // Шашки первого игрока
                    {
                        IChecker checker = new Checker {Color = "B", Point.X = row, Point.Y = col};
                        player1.Checkers.Add(checker); // добавляем шашки первого игрока
                        buttons[row, col].Content = checker;// B - для черных шашек
                        
                    }
                    else if (row > 4 && (row + col) % 2 != 0) // Шашки второго игрока
                    {
                        IChecker checker = new Checker {Color = "W", Point.X = row, Point.Y = col};
                        player2.Checkers.Add(checker);  // добавляем шашки второго игрока
                        buttons[row, col].Content = checker; // W - для белых шашек                       
                    }
                    else
                    {
                        buttons[row, col].Content = null; // пустое поле                    
                    }                                  
                }
            }
        }
        public Button[,] GetButtons() {
            return buttons;
        }
    }
}
