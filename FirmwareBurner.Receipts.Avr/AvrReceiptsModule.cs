using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Receipts.Avr.BurnerFacades;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.Receipts.Avr
{
    /// <summary>Модуль с рецептами для прошивки AVR-устройств</summary>
    public class AvrReceiptsModule : IModule
    {
        private readonly IUnityContainer _container;
        public AvrReceiptsModule(IUnityContainer Container) { _container = Container; }

        public void Initialize()
        {
            _container
                .RegisterType<IBurningToolFacadeFactory<AvrImage>, AvrOverStk500BurningToolFacadeFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IBurningReceiptFactory, gagagagaFactory>("gagaga", new ContainerControlledLifetimeManager());
        }
    }
}
