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
    public class Checker: IChecker
    {
        public String Color { get; set; } 
        public Point Point { get; set; }
        public Checker() {
            Point = new Point();
        }

        //изменение координат шашки
        public void MoveChecker(int x, int y)
        {
            Point.X = x;
            Point.Y = y;
        }

        public override string ToString()
        {
            return Color;
        }
    }

}
