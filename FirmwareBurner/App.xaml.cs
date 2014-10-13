using System.IO;
using System.Windows;
using FirmwareBurner.Burning;
using FirmwareBurner.Burning.Burners.AvrIsp;
using FirmwareBurner.Burning.Burners.AvrIsp.stk500;
using FirmwareBurner.Formating;
using FirmwareBurner.ViewModels;
using FirmwareBurner.ViewModels.Targeting;
using FirmwareBurner.ViewModels.Tools;
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
                    
                    // Конфигурируем индекс
                    .RegisterType<IIndex, ResourceXmlIndex>()

                    .RegisterType<ICellsCatalogProvider, IndexCellsCatalogProvider>(new ContainerControlledLifetimeManager())

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
