using FirmwareBurner.ImageFormatters.Cortex.Catalog;
using FirmwareBurner.Imaging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    public class CortexImagesModule : IModule
    {
        private readonly IUnityContainer _container;
        public CortexImagesModule(IUnityContainer Container) { _container = Container; }

        public void Initialize()
        {
            _container
                .RegisterType<ICortexBootloaderInformationCatalog, CortexBootloaderInformationCatalog>(new ContainerControlledLifetimeManager())
                .RegisterType<IBootloaderConfigurationCatalog, IndexBootloaderConfigurationCatalog>(new ContainerControlledLifetimeManager());

            _container
                .RegisterImageFormattersCatalog<CortexImage, CortexImageFormatterCatalog>()
                .RegisterImageFormattersCatalog<CortexImage, CortexEmptyImageFormatterCatalog>();
        }
    }
}
