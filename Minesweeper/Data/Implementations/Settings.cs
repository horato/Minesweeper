using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Data.Interfaces;

namespace Minesweeper.Data.Implementations
{
    public class Settings : ISettings
    {
        public int GridWidth { get; private set; }
        public int GridLength { get; private set; }
        public int BombsCount { get; private set; }

        public event EventHandler SettingsChanged;

        public void SetWidth(int width)
        {
            GridWidth = width;
            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetLength(int length)
        {
            GridLength = length;
            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetBombsCount(int bombsCount)
        {
            BombsCount = bombsCount;
            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
