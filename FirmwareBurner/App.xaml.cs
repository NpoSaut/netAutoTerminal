using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Unity;
using FirmwareBurner.Formating;

using Path = System.IO.Path;
using FirmwareBurner.Burning;
using FirmwareBurner.Burning.Burners;
using System.IO;

namespace FirmwareBurner
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IUnityContainer container =
                new UnityContainer()
                    .RegisterType<FileInfo>("BootloaderImageFileName", new InjectionConstructor(new InjectionParameter(Path.Combine("Bootloader", "Bootloader"))))
                    .RegisterInstance<IFirmwareFormatter>(XmlFirmwareFormatter.ReadFormat(Path.Combine("Bootloader", "layout.xml")))
                    .RegisterType<FirmwarePacking.SystemsIndexes.Index, FirmwarePacking.SystemsIndexes.XmlIndex>(new InjectionConstructor(new InjectionParameter("BlockKinds.xml")))
                    .RegisterType<IFirmwareBurner, AvrIspBurner>()
                    .RegisterType<IFirmwareCook, FirmwareCook>();


            container.Resolve<FirmwareBurner.MainWindow>().Show();
        }
    }
}
