using FirmwareBurner.ImageFormatters.Avr.Catalog;
using FirmwareBurner.Imaging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.ImageFormatters.Avr
{
    public class AvrImagesModule : IModule
    {
        private readonly IUnityContainer _container;
        public AvrImagesModule(IUnityContainer Container) { _container = Container; }

        public void Initialize()
        {
            _container
                .RegisterType<IAvrBootloadersCatalog, AvrBootloadersCatalog>(new ContainerControlledLifetimeManager())
                .RegisterType<IImageFormattersCatalog<AvrImage>, AvrImageFormatterCatalog>("Раз", new ContainerControlledLifetimeManager());
        }
    }
}
