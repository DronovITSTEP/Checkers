using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЭКЗ_по_Form
{
    internal class Computer: IPlayer
    {
        private List<IChecker> checkers;
        public Computer(List<IChecker> checkers) 
        {
            this.checkers = checkers;
        }
        public void Move(IChecker checker, Point point)//ход игрока
        {
            if (Logic.IsValidMove(point))
                checker.CheckerMove(point.X, point.Y);
        }
        public void Capture(IChecker checker, IChecker enemyChecker, Point point) 
        {
            if (Logic.IsCapture(enemyChecker)) {
                checker.CaptureMove(enemyChecker, point.X, point.Y);
            }
        }
        public void DeleteChecker(IChecker checker) {
            checkers.Remove(checker);
        }
        private void ComputerMove()
        {
            Random rand = new Random(); // генерация случайных чисел
            var possibleMoves = new List<Tuple<int, int, int, int>>(); // создание коллекции для хранения возможных ходов компьютера

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (board[row, col].Player == Player.Player2) // Проверка, принадлежит ли клетка компьютеру
                    {
                        AddPossibleMoves(row, col, possibleMoves); // Заполнение возможных ходов компьютера
                    }
                }
            }

            if (possibleMoves.Count > 0) // Если есть возможные ходы
            {
                // Компьютер случайным образом выбирает один из возможных ходов
                var move = possibleMoves[rand.Next(possibleMoves.Count)];
                MakeMove(move.Item1, move.Item2, move.Item3, move.Item4);
                isPlayer1Turn = true; // переключаемся на игрока

            }
            else
            {
                // Логика в случае, если у компьютера нет доступных ходов (например, окончание игры)
                EndGame();
            }
        }
    }
}
