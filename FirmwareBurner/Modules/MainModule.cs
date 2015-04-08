using ExternalTools.Implementations;
using ExternalTools.Interfaces;
using FirmwareBurner.Interaction;
using FirmwareBurner.Progress;
using FirmwareBurner.Project;
using FirmwareBurner.ViewModels;
using FirmwareBurner.ViewModels.Tools;
using FirmwareBurner.Views;
using FirmwarePacking.SystemsIndexes;
using Microsoft.Practices.Prism.Events;
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

                // Конфигурируем модели представления
                .RegisterType<IProjectViewModelProvider, ProjectViewModelProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IFirmwareSetConstructorViewModelProvider, FirmwareSetConstructorViewModelProvider>()
                .RegisterType<IBurningViewModelFactory, BurningViewModelFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IProjectAssembler, ViewModelProjectAssembler>(new ContainerControlledLifetimeManager())
                .RegisterType<IFileSelectorService, FileRequestServiceViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<IDispatcherFacade, DefaultDispatcher>(new ContainerControlledLifetimeManager())

                // Обработка исключений
                .RegisterType<IExceptionService, EventAggregatorExceptionService>(new ContainerControlledLifetimeManager())
                .RegisterType<IExceptionDialogSource, EventAggregatorExceptionDialogSource>(new ContainerControlledLifetimeManager())

                // Различные утилиты
                .RegisterType<IStringEncoder, Cp1251StringEncoder>(new ContainerControlledLifetimeManager())
                .RegisterType<IProgressControllerFactory, ProgressControllerFactory>(new ContainerControlledLifetimeManager())

                // Инструменты перехода на уровень бизнес-логики
                .RegisterType<IFirmwareProjectFactory, FirmwareProjectFactory>(new ContainerControlledLifetimeManager());

            var regionManager = _container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("Root", typeof (MainView));
        }
    }
}
