using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.BootloadersCatalog
{
    public class XmlBootloaderCatalogModule : IModule
    {
        private readonly IUnityContainer _container;
        public XmlBootloaderCatalogModule(IUnityContainer Container) { _container = Container; }

        public void Initialize()
        {
            _container
                .RegisterType<IBootloadersCatalog, XmlBootloadersCatalog>(new ContainerControlledLifetimeManager());
        }
    }
}
