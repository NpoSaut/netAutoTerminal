using ExternalTools.Implementations;
using ExternalTools.Interfaces;
using FirmwareBurner.Burning;
using FirmwareBurner.Interaction;
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
                .RegisterType<IToolLauncher, ToolLauncher>(new ContainerControlledLifetimeManager())
                    
                // Конфигурируем индекс
                .RegisterType<IIndex, ResourceXmlIndex>()
                .RegisterType<IIndexHelper, IndexHelper>()
                .RegisterType<ICellsCatalogProvider, IndexCellsCatalogProvider>(new ContainerControlledLifetimeManager())

                // Конфигурируем репозитории
                .RegisterType<Repository, DirectoryRepository>("User Repository", new ContainerControlledLifetimeManager(),
                                                               new InjectionConstructor(DirectoryRepository.UserRepositoryDirectory))
                .RegisterType<Repository, DirectoryRepository>("Local Repository", new ContainerControlledLifetimeManager(),
                                                               new InjectionConstructor(DirectoryRepository.ApplicatoinRepositoryDirectory))
                
                // Конфигурируем модели представления
                .RegisterType<IProjectViewModelProvider, ProjectViewModelProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IFirmwareSetConstructorViewModelProvider, FirmwareSetConstructorViewModelProvider>()
                .RegisterType<IBurningViewModelProvider, BurningViewModelProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IProjectValidatorViewModelProvider, ProjectValidatorViewModelProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IProjectAssembler, ViewModelProjectAssembler>(new ContainerControlledLifetimeManager())
                .RegisterType<IFileSelectorService, FileRequestServiceViewModel>(new ContainerControlledLifetimeManager())

                // Инструменты перехода на уровень бизнес-логики
                .RegisterType<IFirmwareProjectFactory, FirmwareProjectFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IBurningReceiptsCatalog, BurningReceiptsCatalog>();

            var regionManager = _container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("Root", typeof (MainView));
        }
    }
}
