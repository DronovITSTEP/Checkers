using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using static ЭКЗ_по_Form.MainWindow;
using System.Diagnostics.Eventing.Reader;

namespace ЭКЗ_по_Form
{
    public class Checker: IMove
    {
        public String Color { get; set; } 
        public IPlayer Player { get; set; }

        //метод для обычного движения шашек
        public bool MakeMove(int fromX, int fromY, int toX, int toY) //выполнение хода
        {
            if (Logic.IsMove())
            {
                MoveChecker();
            }
            else if (Logic.IsCapture())
            {
                CaptureChecker();
            }
        }
        // метод движения с возможностью поедания
        private void MoveChecker(Checker checker, int fromX, int fromY, int toX, int toY)
        {
            //Проверка, что шашка является обычной
            //цикл проходит по всем клеткам между начальной и конечной позициями (fromX и toX).  используется Math.Min и Math.Max, чтобы  обрабатывать движение в любую сторону.

            if (checker.Status == CheckerStatus.Regular &&
                Math.Abs(toX - fromX) == 2 && Math.Abs(toY - fromY) == 2)
            //Math.Abs возвращает абсолютное значение ( без знака), что позволяет проверить разницу между исходной и конечной позицией (по X) на равенство 2.
            //Math.Abs(toX - fromX) == 2: проверяет, перемещается ли шашка на два ряда вниз или вверх по доске.
            //Math.Abs(toY - fromY) == 2)проверяет разницу по горизонтали (по Y) на равенство 2.
            {
                int capturedX = fromX + (toX - fromX) / 2; //Координаты захваченной шашки
                // вычисляет координату X захваченной шашки.
                //  находит среднюю строку между изначальным (fromX) и конечным (toX) положением шашки.
                // Разница toX - fromX делится на 2, чтобы получить  строку, которая находится между ними.
                int capturedY = fromY + (toY - fromY) / 2;
                //тоже самое для Y
                board[capturedX, capturedY].Player = Player.Empty; // удаление шашки
            }

            //Проверка, что шашка является дамой
            //цикл проходит по всем клеткам между начальной и конечной позициями (fromX и toX).  используется Math.Min и Math.Max, чтобы  обрабатывать движение в любую сторону.
            else if (checker.Status == CheckerStatus.King)
            {
                //цикл проходит по всем строкам между начальной (fromX) и целевой (toX) позицией.
                //Math.Min(fromX, toX) + 1 устанавливает начальную точку цикла на первую строку, которая проходит через поедаемую шашку,
                // Math.Max(fromX, toX) указывает на конечную строку. 
                for (int i = Math.Min(fromX, toX) + 1; i < Math.Max(fromX, toX); i++)
                {
                    //определяет координату Y для промежуточного положения.
                    int j = fromY + (toY - fromY) / Math.Abs(toY - fromY);
                    if (board[i, j].Player != Player.Empty)//есть ли в текущей промежуточной клетке шашка. Если в ней есть шашка (не пусто), то:
                    {
                        board[i, j].Player = Player.Empty;//удаление
                    }
                }
            }
        }

        private void CaptureChecker(Checker checker, int fromX, int fromY, int toX, int toY)
        {
            //Проверка, что шашка является обычной
            //цикл проходит по всем клеткам между начальной и конечной позициями (fromX и toX).  используется Math.Min и Math.Max, чтобы  обрабатывать движение в любую сторону.

            if (checker.Status == CheckerStatus.Regular &&
                Math.Abs(toX - fromX) == 2 && Math.Abs(toY - fromY) == 2)
            //Math.Abs возвращает абсолютное значение ( без знака), что позволяет проверить разницу между исходной и конечной позицией (по X) на равенство 2.
            //Math.Abs(toX - fromX) == 2: проверяет, перемещается ли шашка на два ряда вниз или вверх по доске.
            //Math.Abs(toY - fromY) == 2)проверяет разницу по горизонтали (по Y) на равенство 2.
            {
                int capturedX = fromX + (toX - fromX) / 2; //Координаты захваченной шашки
                // вычисляет координату X захваченной шашки.
                //  находит среднюю строку между изначальным (fromX) и конечным (toX) положением шашки.
                // Разница toX - fromX делится на 2, чтобы получить  строку, которая находится между ними.
                int capturedY = fromY + (toY - fromY) / 2;
                //тоже самое для Y
                board[capturedX, capturedY].Player = Player.Empty; // удаление шашки
            }

            //Проверка, что шашка является дамой
            //цикл проходит по всем клеткам между начальной и конечной позициями (fromX и toX).  используется Math.Min и Math.Max, чтобы  обрабатывать движение в любую сторону.
            else if (checker.Status == CheckerStatus.King)
            {
                //цикл проходит по всем строкам между начальной (fromX) и целевой (toX) позицией.
                //Math.Min(fromX, toX) + 1 устанавливает начальную точку цикла на первую строку, которая проходит через поедаемую шашку,
                // Math.Max(fromX, toX) указывает на конечную строку. 
                for (int i = Math.Min(fromX, toX) + 1; i < Math.Max(fromX, toX); i++)
                {
                    //определяет координату Y для промежуточного положения.
                    int j = fromY + (toY - fromY) / Math.Abs(toY - fromY);
                    if (board[i, j].Player != Player.Empty)//есть ли в текущей промежуточной клетке шашка. Если в ней есть шашка (не пусто), то:
                    {
                        board[i, j].Player = Player.Empty;//удаление
                    }
                }
            }
        }
        public CheckerStatus Status { get; set; }

        public override string ToString()
        {
            return Color;
        }

    }

}
