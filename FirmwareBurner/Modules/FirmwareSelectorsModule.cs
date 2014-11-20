using FirmwareBurner.ViewModels.FirmwareSources;
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
            _container.RegisterType<IFirmwareSelectorViewModelProvider, CompositeFirmwareSelectorViewModelProvider>(new ContainerControlledLifetimeManager());
        }
    }
}
