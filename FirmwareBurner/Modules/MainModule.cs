using System.IO;
using FirmwareBurner.Burning;
using FirmwareBurner.Burning.Burners.AvrIsp;
using FirmwareBurner.Burning.Burners.AvrIsp.stk500;
using FirmwareBurner.ViewModels;
using FirmwareBurner.ViewModels.Tools;
using FirmwareBurner.Views;
using FirmwarePacking.Repositories;
using FirmwarePacking.SystemsIndexes;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.Modules
{
    public class MainModule : IModule
    {
        private readonly IUnityContainer _container;
        public MainModule(IUnityContainer Container) { _container = Container; }

        public void Initialize()
        {
            _container
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
                .RegisterType<IFirmwareCook, FirmwareCook>()
                
                .RegisterType<IProjectViewModelProvider, ProjectViewModelProvider>(new ContainerControlledLifetimeManager());

            var regionManager = _container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("Root", typeof (MainView));
        }
    }
}
