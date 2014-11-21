using System.IO;
using FirmwareBurner.Project;
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
                .RegisterType<IFirmwareCook, FirmwareCook>()
                
                // Конфигурируем модели представления
                .RegisterType<IProjectViewModelProvider, ProjectViewModelProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IFirmwareSetConstructorViewModelProvider, FirmwareSetConstructorViewModelProvider>()
                .RegisterType<IProjectAssembler, ViewModelProjectAssembler>(new ContainerControlledLifetimeManager())

                // Инструменты перехода на уровень бизнес-логики
                .RegisterType<IFirmwareProjectFactory, FirmwareProjectFactory>(new ContainerControlledLifetimeManager());

            var regionManager = _container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("Root", typeof (MainView));
        }
    }
}
