using System.IO;
using System.Windows;
using FirmwareBurner.Burning;
using FirmwareBurner.Burning.Burners.AvrIsp;
using FirmwareBurner.Burning.Burners.AvrIsp.stk500;
using FirmwareBurner.Formating;
using FirmwarePacking.Repositories;
using FirmwarePacking.SystemsIndexes;
using Microsoft.Practices.Unity;

namespace FirmwareBurner
{
    /// <summary>Логика взаимодействия для App.xaml</summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IUnityContainer container =
                new UnityContainer()
                    .RegisterType<FileInfo>("BootloaderImageFileName",
                                            new InjectionConstructor(new InjectionParameter(Path.Combine("Bootloader", "Bootloader"))))
                    .RegisterInstance(XmlFirmwareFormatter.ReadFormat(Path.Combine("Bootloader", "layout.xml")))
                    
                    // Конфигурируем индекс
                    .RegisterType<Index, ResourceXmlIndex>()

                    // Конфигурируем репозитории
                    .RegisterType<Repository, DirectoryRepository>("User Repository", new ContainerControlledLifetimeManager(),
                                                                   new InjectionConstructor(DirectoryRepository.UserRepositoryDirectory))
                    .RegisterType<Repository, DirectoryRepository>("Local Repository", new ContainerControlledLifetimeManager(),
                                                                   new InjectionConstructor(DirectoryRepository.ApplicatoinRepositoryDirectory))

                    // Конфигурируем прожигателей
                    .RegisterType<IFirmwareBurner, AvrIspBurner>()
                    .RegisterType<IAvrIspCommandShell, Stk500>()
                    .RegisterType<IFirmwareCook, FirmwareCook>();

            container.Resolve<MainWindow>().Show();
        }
    }
}
