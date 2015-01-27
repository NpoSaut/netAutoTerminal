using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.Unity;
using FirmwarePacker.Dialogs;
using FirmwarePacker.Models;

namespace FirmwarePacker
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public String ArgsDirectory { get; set; }
        public static App CurrentApp { get { return Current as App; } }
        public String AppDirectory { get { return AppDomain.CurrentDomain.BaseDirectory; } }

        protected override void OnStartup(StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += CurrentOnDispatcherUnhandledException;
            base.OnStartup(e);

            if (e.Args.Length > 0) ArgsDirectory = e.Args[0];

            MainWindow mainWindow = ServiceLocator.Container.Resolve<FirmwarePacker.MainWindow>();
            mainWindow.Show();
        }

        private void CurrentOnDispatcherUnhandledException(object Sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (MessageBox.Show(
                String.Format("Непредвиденная ошибка при работе приложения:\n{0}\nСкопировать сведения об ошибке в буфер обмена?", e.Exception),
                "Ошибка в приложении", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                Clipboard.SetText(e.Exception.ToString());
        }
    }

    public static class ServiceLocator
    {
        private static readonly IUnityContainer _Container;
        public static IUnityContainer Container
        {
            get { return _Container; }
        }

        static ServiceLocator()
        {
            _Container = new UnityContainer()
                .RegisterType<IFileSelector, DialogFileSelector>()
                .RegisterType<IDirectorySelector, DialogDirectorySelector>()
                .RegisterType<FirmwareTreeViewModel>(new InjectionConstructor())
                .RegisterType<FirmwarePacking.SystemsIndexes.Index, FirmwarePacking.SystemsIndexes.ResourceXmlIndex>();
        }
    }
}
