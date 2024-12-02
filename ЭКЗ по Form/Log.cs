using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ЭКЗ_по_Form
{
    internal class Log
    {

        private void SaveStatistics(int wins, int losses)
        {
            using (StreamWriter sw = new StreamWriter("statistics.txt", true))
            {
                sw.WriteLine($"Победы: {wins}, Поражения: {losses}, Дата: {DateTime.Now}");
            }
        }

        private void ShowStatistics()
        {
            if (File.Exists("statistics.txt"))
            {
                string stats = File.ReadAllText("statistics.txt");
                MessageBox.Show(stats, "Статистика");
            }
            else
            {
                MessageBox.Show("Нет доступных данных о статистике.", "Статистика");
            }
        }

        private void SaveGameState()
        {
            using (StreamWriter sw = new StreamWriter("gamestate.txt"))
            {
                // Сохранение состояния доски
                for (int row = 0; row < BoardSize; row++)
                {
                    for (int col = 0; col < BoardSize; col++)
                    {
                        sw.Write(board[row, col].Player + ",");
                    }
                    sw.WriteLine();
                }
                // Сохранение текущего игрока
                sw.WriteLine(isPlayer1Turn); // Сохранение состояния переменной isPlayer1Turn
            }
        }
        private void LoadGameState()
        {
            if (File.Exists("gamestate.txt")) // Проверка существования файла
            {
                using (StreamReader sr = new StreamReader("gamestate.txt")) // Объект StreamReader для чтения файла
                {
                    // Чтение состояния доски
                    for (int row = 0; row < BoardSize; row++)
                    {
                        string[] values = sr.ReadLine().Split(',');
                        for (int col = 0; col < BoardSize; col++)
                        {
                            board[row, col] = new Checker
                            {
                                Player = Enum.TryParse(values[col], out Player player) ? player : Player.Empty
                            };
                        }
                    }

                    // Загрузка состояния текущего игрока
                    string isPlayer1TurnValue = sr.ReadLine();
                    isPlayer1Turn = bool.TryParse(isPlayer1TurnValue, out bool isTurn) && isTurn;
                }
            }
        }
    }
}
