using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЭКЗ_по_Form
{
    internal interface IChecker
    {
        String Color { get; set; } 
        Point Point { get; set; }
        void MoveChecker(int x, int y);
    }
}
