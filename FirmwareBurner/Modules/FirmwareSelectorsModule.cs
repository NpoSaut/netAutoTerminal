using FirmwareBurner.ViewModels.FirmwareSources;
using FirmwareBurner.ViewModels.FirmwareSources.LoadControllers;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.Modules
{
    public class FirmwareSelectorsModule : IModule
    {
        private readonly IUnityContainer _container;
        public FirmwareSelectorsModule(IUnityContainer Container) { _container = Container; }

        public void Initialize()
        {
            _container.RegisterType<ILoadControllerFactory, LoadControllerFactory>(new ContainerControlledLifetimeManager())
                //.RegisterType<IFirmwareSelectorViewModelProvider, FakeIntegratedFirmwareSelectorViewModelProvider>(
                //    new ContainerControlledLifetimeManager())
                      .RegisterType<IFirmwareSelectorViewModelProvider, IntegratedFirmwareSelectorViewModelProvider>(new ContainerControlledLifetimeManager())
                      .RegisterType<IFirmwarePackageViewModelKeyFormatter, FirmwarePackageViewModelKeyFormatter>(new ContainerControlledLifetimeManager())
                      .RegisterType<IFirmwarePackageViewModelFactory, FirmwarePackageViewModelFactory>(new ContainerControlledLifetimeManager());
        }
    }
}
