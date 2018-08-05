using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Data.Interfaces
{
    public interface ISettings
    {
        int GridWidth { get; }
        int GridLength { get; }
        int BombsCount { get; }
        event EventHandler SettingsChanged;

        void SetWidth(int width);
        void SetLength(int length);
        void SetBombsCount(int bombsCount);
    }
}
