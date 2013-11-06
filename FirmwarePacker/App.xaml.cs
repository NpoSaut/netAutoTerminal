using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
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
            base.OnStartup(e);

            if (e.Args.Length > 0) ArgsDirectory = e.Args[0];

            MainWindow mainWindow = ServiceLocator.Container.Resolve<FirmwarePacker.MainWindow>();
            mainWindow.Show();
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
                .RegisterType<FirmwarePacking.SystemsIndexes.Index, FirmwarePacking.SystemsIndexes.XmlIndex>(
                                                                                        new ContainerControlledLifetimeManager(),
                                                                                        new InjectionConstructor(Path.Combine(App.CurrentApp.AppDirectory, "BlockKinds.xml")));
        }
    }
}
