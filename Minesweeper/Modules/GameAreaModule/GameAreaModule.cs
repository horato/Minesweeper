using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Minesweeper.Data;
using Minesweeper.Data.Constants;
using Minesweeper.Modules.GameAreaModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace Minesweeper.Modules.GameAreaModule
{
    public class GameAreaModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public GameAreaModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.GameAreaModule, typeof(GameAreaView));
        }
    }
}
