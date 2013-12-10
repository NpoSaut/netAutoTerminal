using System.Windows;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Unity;
using FirmwareBurner.Formating;

using Path = System.IO.Path;
using FirmwareBurner.Burning;
using System.IO;
using FirmwareBurner.Burning.Burners.AvrIsp;

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
                    
                    // Конфигурируем индекс
                    .RegisterType<FirmwarePacking.SystemsIndexes.Index, FirmwarePacking.SystemsIndexes.ResourceXmlIndex>()

                    // Конфигурируем репозитории
                    .RegisterType<Repository, DirectoryRepository>("User Repository", new ContainerControlledLifetimeManager(), new InjectionConstructor(DirectoryRepository.UserRepositoryDirectory))
                    .RegisterType<Repository, DirectoryRepository>("Local Repository", new ContainerControlledLifetimeManager(), new InjectionConstructor(DirectoryRepository.ApplicatoinRepositoryDirectory))

                    // Конфигурируем прожигателей
                    .RegisterType<IFirmwareBurner, AvrIspBurner>()
                    .RegisterType<IAvrIspCommandShell, FirmwareBurner.Burning.Burners.AvrIsp.stk500.Stk500>()
                    .RegisterType<IFirmwareCook, FirmwareCook>();


            container.Resolve<FirmwareBurner.MainWindow>().Show();
        }
    }
}
