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
        private Button[,] buttons = new Button[SIZE,SIZE];
        public void StartGame()
        {
            MessageBox.Show("Игра началась");
        }

        public Button[,] GetButtons(IPlayer player1, IPlayer player2)
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
                        buttons[row, col].Content = new Checker { Player = player1, Color = "B" };// B - для черных шашек
                        
                    }
                    else if (row > 4 && (row + col) % 2 != 0) // Шашки второго игрока
                    {
                        buttons[row, col].Content = new Checker { Player = player2, Color = "W" }; // W - для белых шашек                       
                    }
                    else
                    {
                        buttons[row, col].Content = null; // пустое поле                    
                    }                                  
                }
            }
            return buttons;
        }
    }
}
