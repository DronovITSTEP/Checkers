using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЭКЗ_по_Form
{
    public interface IPlayer
    {
        void Move(IPlayer enemyPlayer, IChecker checker, Point point);
    }
}
