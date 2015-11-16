using FirmwarePacker.Dialogs;
using FirmwarePacker.Enpacking;
using FirmwarePacker.Project.Serializers;
using FirmwarePacker.Views;
using FirmwarePacking.SystemsIndexes;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace FirmwarePacker
{
    public class MainModule : IModule
    {
        private readonly IUnityContainer _container;
        public MainModule(IUnityContainer Container) { _container = Container; }

        public void Initialize()
        {
            _container
                .RegisterType<IFileSelector, DialogFileSelector>(new ContainerControlledLifetimeManager())
                .RegisterType<IDirectorySelector, DialogDirectorySelector>(new ContainerControlledLifetimeManager())
                .RegisterType<IIndex, ResourceXmlIndex>(new ContainerControlledLifetimeManager())
                .RegisterType<IIndexHelper, IndexHelper>(new ContainerControlledLifetimeManager())
                .RegisterType<IVariablesProcessor, VariablesProcessor>(new ContainerControlledLifetimeManager())
                .RegisterType<IProjectSerializer, XmlProjectSerializer>(new ContainerControlledLifetimeManager())
                .RegisterType<IEnpacker, Enpacker>(new ContainerControlledLifetimeManager())
                .RegisterType<IPackageSavingTool, PackageSavingTool>(new ContainerControlledLifetimeManager())
                .RegisterType<IPackageSavingService, PackageSavingService>(new ContainerControlledLifetimeManager());

            var regionManager = _container.Resolve<RegionManager>();
            regionManager.RegisterViewWithRegion("MainView", typeof (MainView));
        }
    }
}
