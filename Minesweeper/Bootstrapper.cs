using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Minesweeper.Data.Implementations;
using Minesweeper.Data.Interfaces;
using Minesweeper.Data.Logic;
using Minesweeper.Modules.GameAreaModule;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;

namespace Minesweeper
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            ViewModelLocationProvider.SetDefaultViewModelFactory(x => Container.Resolve(x));

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(MainBarModule));
            catalog.AddModule(typeof(GameAreaModule));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            RegisterTypeIfMissing(typeof(ISettings), typeof(Settings), true);
            RegisterTypeIfMissing(typeof(IGameGrid), typeof(GameGrid), true);
        }
    }
}
