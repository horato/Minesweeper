using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Data.Interfaces
{
    public interface IGameGrid
    {
        void Initialize();
        bool IsGameCompleted();
    }
}
