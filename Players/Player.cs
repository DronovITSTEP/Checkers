using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЭКЗ_по_Form
{
    internal class Player: IPlayer
    {
        public List<IChecker> Checkers;
        public Player() 
        {
            Checkers = new List<IChecker>();
        }
        // ход игрока
        public void Move(IPlayer enemyPlayer, IChecker checker, Point point)
        {
            // обычный ход
            if (Logic.IsValidMove(checker, point)) {
                checker.CheckerMove(point.X, point.Y);
            }
            // ход с поеданием
            if (Logic.IsCapture(checker, point)) {
                checker.CheckerMove(point.X, point.Y);
                DeleteEnemyChecker(enemyPlayer, enemyChecker);
            }
            // переход в дамки
            if (Logic.IsMakeQueen(checker)) {
                checker = checker as QueenChecker;
            }
        }

        private void DeleteChecker(IChecker checker) 
        {
            checkers.Remove(checker);
        }
        private void DeleteEnemyChecker(IPlayer enemy, IChecker enemyChecker) 
        {
            enemy.DeleteChecker(enemyChecker);
        }
    }
}
