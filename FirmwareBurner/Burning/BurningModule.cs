using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.Burning
{
    public class BurningModule : IModule
    {
        private readonly IUnityContainer _container;
        public BurningModule(IUnityContainer Container) { _container = Container; }

        public void Initialize()
        {
            _container.RegisterType<IBurningService, BurningService>(new ContainerControlledLifetimeManager())
                      .RegisterType<IBurningReceiptsCatalog, BurningReceiptsCatalog>(new ContainerControlledLifetimeManager());
        }
    }
}
