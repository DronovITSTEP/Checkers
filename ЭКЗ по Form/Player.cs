using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЭКЗ_по_Form
{
    internal class Player: IPlayer
    {
        private void PlayerMove(int fromRow, int fromCol, int toRow, int toCol)//ход игрока
        {
            //   логика хода игрока
            if (isPlayer1Turn) // Проверка, что ходит первый игрок
            {
                MakeMove(fromRow, fromCol, toRow, toCol); // выполнение хода игрока
                isPlayer1Turn = false; // После хода игрока  ход компьютера

                // Проверка на наличие возможных ходов у компьютера
                if (isAgainstComputer)
                {
                    ComputerMove(); // Компьютер делает ход
                }
            }
        }

        //Перечисление, которое определяет возможные состояния для шашек: пустая клетка и шашки для первого и второго игроков.
        public enum Player
        {
            Empty,
            Player1,
            Player2
        }
    }
}
