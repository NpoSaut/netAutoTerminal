using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr.BurnerFacades;
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
                .RegisterType<IImageFormatterFactory<AvrImage>, AvrImageFormatterFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IPropertiesTableGenerator, PropertiesTableGenerator>(new ContainerControlledLifetimeManager())
                
                .RegisterType<IBurningToolFacadeFactory<AvrImage>, AvrOverStk500BurningToolFacadeFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IBurningReceiptFactory, gagagagaFactory>("gagaga", new ContainerControlledLifetimeManager());
        }
    }

    public class gagagagaFactory : BurningReceiptFactory<AvrImage>
    {
        public gagagagaFactory(IImageFormatterFactory<AvrImage> ImageFormatterFactory, IBurningToolFacadeFactory<AvrImage> ToolFacadeFactory) : base(ImageFormatterFactory, ToolFacadeFactory) { }
    }
}
