using System;
using System.Windows;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Modules;
using FirmwareBurner.Receipts.Avr;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace FirmwareBurner
{
    public class BurnerBootstrapper : UnityBootstrapper, IDisposable
    {
        /// <summary>
        ///     Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых
        ///     ресурсов.
        /// </summary>
        public void Dispose() { Container.Dispose(); }

        /// <summary>Configures the <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog" /> used by Prism.</summary>
        protected override void ConfigureModuleCatalog()
        {
            var mc = (ModuleCatalog)ModuleCatalog;

            // Общие модули
            mc.AddModule(typeof (CommonsModule));

            // Модули сборщиков образов
            mc.AddModule(typeof (AvrImagesModule));

            // Модули рецептов прошивки
            mc.AddModule(typeof (AvrReceiptsModule));

            // Модули интерфейса
            mc.AddModule(typeof (FirmwareSelectorsModule));
            mc.AddModule(typeof (MainModule));

            base.ConfigureModuleCatalog();
        }

        /// <summary>Creates the shell or main window of the application.</summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        ///     If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        ///     <see cref="T:Microsoft.Practices.Prism.Bootstrapper" /> will attach the default
        ///     <seealso cref="T:Microsoft.Practices.Prism.Regions.IRegionManager" /> of the application in its
        ///     <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty" /> attached property in order
        ///     to be able to add regions by using the
        ///     <seealso cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty" />
        ///     attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            var mainWindow = Container.Resolve<Shell>();
            return mainWindow;
        }

        /// <summary>Initializes the shell.</summary>
        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
