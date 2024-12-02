using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЭКЗ_по_Form
{
    internal interface IMove
    {
        bool MakeMove(int fromX, int fromY, int toX, int toY);
        bool CaptureCheckers(Checker checker, int fromX, int fromY, int toX, int toY);
    }
}
