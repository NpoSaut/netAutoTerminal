using System.Windows;
using FirmwarePacker.LaunchParameters;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace FirmwarePacker
{
    public class PackerBootstrapper : UnityBootstrapper
    {
        private readonly string[] _startArguments;
        public PackerBootstrapper(string[] StartArguments) { _startArguments = StartArguments; }

        protected override void ConfigureModuleCatalog()
        {
            var mc = (ModuleCatalog)ModuleCatalog;

            // Общие модули
            mc.AddModule(typeof (MainModule));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterInstance<ILaunchParameters>(new CommandLineLaunchParameters(_startArguments));
        }

        protected override DependencyObject CreateShell() { return Container.Resolve<MainWindow>(); }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
