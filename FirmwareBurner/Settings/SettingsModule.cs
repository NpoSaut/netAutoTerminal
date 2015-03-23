using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.Settings
{
    public class SettingsModule : IModule
    {
        private readonly IUnityContainer _container;
        public SettingsModule(IUnityContainer Container) { _container = Container; }

        public void Initialize() { _container.RegisterType<ISettingsService, PropertiesSettingsService>(new ContainerControlledLifetimeManager()); }
    }
}
