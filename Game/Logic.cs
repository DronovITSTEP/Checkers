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
        // можно ли переместить шашку на выбранные координаты
        public static bool IsValidMove(IChecker checker, Point point) {
            // для обычной шашки
            if (checker is Checker)
                return (Math.Abs(checker.Point.X - point.X) == 1 && Math.Abs(checker.Point.Y - point.Y) == 1);

            // для дамки
            else if (checker is QueenChecker)
                return (Math.Abs(checker.Point.X - point.X) == Math.Abs(checker.Point.Y - point.Y))
            
            return false;
        }

        // можно ли съесть шашку
        public static bool IsCapture(IChecker checker, Point point) {
            return (Math.Abs(checker.Point.X - point.X) == 2 && Math.Abs(checker.Point.Y - point.Y) == 2);
        }

        // может ли шашка стать дамкой
        public static bool IsMakeQueen(IChecker checker) {
            if (checker.Color == "B" && checker.Point.X == 8) {
                return true;
            }

            if (checker.Color == "W" && checker.Point.X == 0) {
                return true;
            }
            return false;
        }
    }
}
