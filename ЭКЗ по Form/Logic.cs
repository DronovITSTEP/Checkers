using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static ЭКЗ_по_Form.MainWindow;

namespace ЭКЗ_по_Form
{
    static class Logic
    {
        private int wins = 0; // Победы
        private int losses = 0; // Поражения
        private bool isPlayer1Turn = true;  //bool значение чей ход сейчас
        private void EndGame()
        {
            // Логика завершения игры
            MessageBox.Show("Игра окончена!");
        }
        private void CheckMove(int fromRow, int fromCol, int toRow, int toCol, List<Tuple<int, int, int, int>> possibleMoves) // проверка является ли ход допустимым
        {
            // Условие для проверки, находится ли целевая позиция (toRow, toCol) на поле.
            if (toRow >= 0 && toRow < BoardSize && toCol >= 0 && toCol < BoardSize)
            {
                // Проверка, свободна ли ячейка (занимает ли ее игрок). Если на этой позиции находится игрок Player.Empty, значит место свободно.
                if (board[toRow, toCol].Player == Player.Empty)
                {
                    // Если ячейка свободна, то возможный ход записывается в список possibleMoves.
                    possibleMoves.Add(new Tuple<int, int, int, int>(fromRow, fromCol, toRow, toCol));
                }

                // Проверка на поедание - необходимо, чтобы перемещение происходило на две клетки диагонально.
                if (Math.Abs(toRow - fromRow) == 2 && Math.Abs(toCol - fromCol) == 2)
                {
                    // Вычисляется строка и столбец позиции фигуры, которая может быть съедена.
                    int capturedX = fromRow + (toRow - fromRow) / 2;
                    int capturedY = fromCol + (toCol - fromCol) / 2;

                    // Проверка, находится ли на позиции, возможной для поедания, фигура соперника (не пуста и не своего игрока).
                    if (capturedX >= 0 && capturedX < BoardSize && capturedY >= 0 && capturedY < BoardSize &&
                        board[capturedX, capturedY].Player != Player.Empty &&
                        board[capturedX, capturedY].Player != (isPlayer1Turn ? Player.Player1 : Player.Player2))
                    {
                        // Если поедание возможно, то этот ход добавляется в список possibleMoves.
                        possibleMoves.Add(new Tuple<int, int, int, int>(fromRow, fromCol, toRow, toCol));
                    }
                }
            }
        }
        private void CheckCaptureMoves(int row, int col, List<Tuple<int, int, int, int>> possibleMoves) //проверят возможные для съедания шашки
        {
            int[] directions = { -1, 1 }; //массив целых чисел, представляющий направления, в которых может двигаться дамка.
                                          //-1 указывает движение вверх по доске. дамка перемещается от игрока 2 к игроку 1).
                                          //            1 указывает движение вниз по доске. дамка перемещается от игрока 1 к игроку 2).
                                          // позволяет проверять ходы в обе стороны.
            foreach (var dir in directions)//перебирает каждое значение из массива directions dir-переменые от -1 до 1
            {
                // Левое поедание
                //row + dir * 2 — это  строка для захвата, которая рассчитывается, добавляя 2 к текущей строке  dir. Если dir равен -1, то это будет означать row - 2; если dir равен 1, то это будет row + 2.
                //col - 2 — это  столбец для захвата, который всегда сдвигается на 2 влево.
                //possibleMoves — это список, в который будут добавляться возможные ходы.
                CheckCapture(row, col, row + dir * 2, col - 2, dir, possibleMoves);

                // Правое поедание
                CheckCapture(row, col, row + dir * 2, col + 2, dir, possibleMoves);

            }
        }
        private void CheckCapture(int fromRow, int fromCol, int toRow, int toCol, int direction, List<Tuple<int, int, int, int>> possibleMoves)//проверка можно ли съесть фигуру противника (
        {
            // Проверка на границы для целевой клетки
            if (toRow >= 0 && toRow < BoardSize && toCol >= 0 && toCol < BoardSize)
            //проверка, находится ли целевая клетка (toRow, toCol) в пределах доски
            //Если toRow меньше 0 или больше или равно BoardSize, тогда ход выходит за границы доски, и выполнение метода продолжено не будет.
            {
                // Проверка на границы для клетки, которую предполагается "съесть"

                int midRow = fromRow + direction;// вычисление координаты строки для средней клетки, которую предполагается "съесть".
                                                 // добавление значения direction к текущей строке fromRow.  direction может быть 1 для хода вниз и -1 для хода вверх 
                int midCol = fromCol + direction;

                // координаты клетки, которая потенциально будет "съедена" при выполнении хода. direction указывает, насколько
                // здесь должно быть смещение от начальной клетки для получения средней клетки, которая будет проверяться ,
                //если фигурка двигается на 2 клетки, direction может быть 1 или -1, чтобы  рассчитать координаты.


                if (midRow >= 0 && midRow < BoardSize && midCol >= 0 && midCol < BoardSize)//Проверка, находится ли клетка midRow, midCol  также в пределах доски.
                //Это предотвращает обращение к несуществующему индексу (были "вылеты")
                {
                    if (board[toRow, toCol].Player == Player.Empty &&//, проверка, можно ли сделать ход.  является ли целевая клетка (toRow, toCol) пустой (Player.Empty), чтобы туда можно было переместить шашку.
                        board[midRow, midCol].Player != Player.Empty && //занята ли средней клетка фигурой. Если она не пустая, значит, там находится шашка, которую можно съесть.
                        board[midRow, midCol].Player != (isPlayer1Turn ? Player.Player1 : Player.Player2))
                    //проверка шашки, которая потенциально будет съедена, принадлежит сопернику. вопросительный оператор для выбора игрока в зависимости от текущего хода (isPlayer1Turn).
                    {
                        possibleMoves.Add(new Tuple<int, int, int, int>(fromRow, fromCol, toRow, toCol));
                    }
                    // все предыдущие проверки прошли успешно, ход добавляется в список возможных ходов (possibleMoves). начальная позиция (fromRow, fromCol) и конечная позиция (toRow, toCol).
                    //означает, что данное перемещение — допустимое.
                }
            }
        }
        //проверка является ли ход допустимым
        private bool IsValidMove(Checker checker, int fromX, int fromY, int toX, int toY)
        {
            if (checker.Status == CheckerStatus.Regular)
            // проверка, является ли  шашка обычной, проверяет допустимость хода для обычной шашки.
            {
                //шашка принадлежит первому игроку (Player1), и она пытается двигаться вверх по доске (по оси Y), что недопустимо, возвращает false 

                if (checker.Player == Player.Player1 && toX < fromX) return false;

                //аналогично 
                if (checker.Player == Player.Player2 && toX > fromX) return false;
                //Проверяет, что шашка перемещается на одну клетку по диагонали. если нет-false
                if (Math.Abs(toX - fromX) != 1 || Math.Abs(toY - fromY) != 1) return false;
                return board[toX, toY].Player == Player.Empty; // проверка на свободное место
            }
            else
            {
                // Движение королевы
                //перемещение происходит по диагонали на любое количество клеток. если нет- false.
                if (Math.Abs(toX - fromX) != Math.Abs(toY - fromY)) return false;
                //цикл, который проходит по всем клеткам между начальной и конечной позициями. Используется (toX - fromX) / Math.Abs(toX - fromX) для определения направления -вверх или вниз п.
                // i != toX -  цикл завершится, когда достигнет конечной ячейки.
                for (int i = fromX + (toX - fromX) / Math.Abs(toX - fromX); i != toX; i += (toX - fromX) / Math.Abs(toX - fromX))
                {
                    //попадает на клетку, в которой есть шашка ( не пустая)- false.
                    if (board[i, fromY + (toY - fromY) / Math.Abs(toY - fromY)].Player != Player.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void CheckForWin()
        {
            // Проверка на выигрыш
            bool player1HasCheckers = false;
            bool player2HasCheckers = false;

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (board[row, col].Player == Player.Player1)
                        player1HasCheckers = true;
                    else if (board[row, col].Player == Player.Player2)
                        player2HasCheckers = true;
                }
            }

            if (!player1HasCheckers)
            {
                MessageBox.Show("Игрок 2 выиграл!");
                Application.Current.Shutdown();
            }
            else if (!player2HasCheckers)
            {
                MessageBox.Show("Игрок 1 выиграл!");
                Application.Current.Shutdown();
            }
        }
    }
}
