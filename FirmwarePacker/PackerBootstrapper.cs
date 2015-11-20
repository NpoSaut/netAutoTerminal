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
        private ILaunchParameters _launchParameters;
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
            _launchParameters = new CommandLineLaunchParameters(_startArguments);
            Container.RegisterInstance(_launchParameters);
        }

        protected override DependencyObject CreateShell()
        {
            return _launchParameters.SilentMode
                       ? null
                       : Container.Resolve<MainWindow>();
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);
            if (_launchParameters.SilentMode)
            {
                Container.Resolve<SilentPacker>().Run();
                Application.Current.Shutdown();
            }
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
