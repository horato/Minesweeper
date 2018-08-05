using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Data.Enum
{
    public enum CellState
    {
        None = 1,
        Flag = 2,
        Bomb = 3,
        Text = 4
    }
}
