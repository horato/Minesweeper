using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Data.Interfaces;
using Prism.Mvvm;

namespace Minesweeper.Modules.MainBarModule.ViewModels
{
    public class MainBarViewModel : BindableBase
    {
        private readonly ISettings _settings;
        private const int MAX_WIDTH = 20;
        private const int DEFAULT_WIDTH = 10;
        private const int MAX_LENGTH = 20;
        private const int DEFAULT_LENGTH = 10;

        public int GridWidth
        {
            get { return _settings.GridWidth; }
            set
            {
                _settings.SetWidth(value > MAX_WIDTH ? MAX_WIDTH : value);
                UpdateBombsCount();
                RaisePropertyChanged(nameof(GridWidth));
            }
        }

        public int GridLength
        {
            get { return _settings.GridLength; }
            set
            {
                _settings.SetLength(value > MAX_LENGTH ? MAX_LENGTH : value);
                UpdateBombsCount();
                RaisePropertyChanged(nameof(GridLength));
            }
        }

        public int BombsCount
        {
            get { return _settings.BombsCount; }
            set
            {
                _settings.SetBombsCount(value);
                RaisePropertyChanged(nameof(BombsCount));
            }
        }

        public MainBarViewModel(ISettings settings)
        {
            _settings = settings;

            GridWidth = DEFAULT_WIDTH;
            GridLength = DEFAULT_LENGTH;
            UpdateBombsCount();
        }

        private void UpdateBombsCount()
        {
            BombsCount = (GridWidth * GridLength) / 10;
        }
    }
}
