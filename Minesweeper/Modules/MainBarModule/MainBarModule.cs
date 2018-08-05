using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Minesweeper.Data;
using Minesweeper.Data.Constants;
using Minesweeper.Modules.GameAreaModule.Views;
using Minesweeper.Modules.MainBarModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace Minesweeper.Modules.GameAreaModule
{
    public class MainBarModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MainBarModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainBarModule, typeof(MainBarView));
        }
    }
}
