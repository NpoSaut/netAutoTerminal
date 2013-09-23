using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

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
                .RegisterType<FirmwareTreeModel>(new InjectionConstructor())
                .RegisterType<FirmwarePacking.SystemsIndexes.Index, FirmwarePacking.SystemsIndexes.XmlIndex>(
                                                                                        new ContainerControlledLifetimeManager(),
                                                                                        new InjectionConstructor("BlockKinds.xml"));
        }
    }
}
