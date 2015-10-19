using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Cortex;
using FirmwareBurner.Receipts.Cortex.BurnerFacades;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.Receipts.Cortex
{
    public class CortexReceiptsModule : IModule
    {
        private readonly IUnityContainer _container;
        public CortexReceiptsModule(IUnityContainer Container) { _container = Container; }

        public void Initialize()
        {
            _container
                .RegisterBurningReceiptFactory<CortexImage, CortexToHexFileToolFacadeFactory>();
        }
    }
}
