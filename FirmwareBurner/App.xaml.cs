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
                    .RegisterInstance<IFirmwareFormatter>(XmlFirmwareFormatter.ReadFormat(Path.Combine("Bootloader", "layout.xml")))
                    .RegisterType<IFirmwareBurner, AvrIspBurner>();
        }
    }
}
