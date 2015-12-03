using FirmwareBurner.ImageFormatters.Cortex;
using FirmwareBurner.ImageFormatters.UskUsb.Catalog;
using FirmwareBurner.Imaging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.ImageFormatters.UskUsb
{
    public class UskUsbImagesModule : IModule
    {
        private readonly IUnityContainer _container;
        public UskUsbImagesModule(IUnityContainer Container) { _container = Container; }

        public void Initialize()
        {
            _container
                .RegisterType<IUskUsbBootloadersCatalog, UskUsbBootloadersCatalog>();

            _container
                .RegisterType<IImageFormattersCatalog<CortexImage>, UskUsbImageFormatterCatalog>("Раз", new ContainerControlledLifetimeManager());
        }
    }
}
