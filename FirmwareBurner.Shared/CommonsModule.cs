using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FirmwareBurner
{
    public class CommonsModule : IModule
    {
        private readonly IUnityContainer _container;
        public CommonsModule(IUnityContainer Container) { _container = Container; }

        /// <summary>Notifies the module that it has be initialized.</summary>
        public void Initialize()
        {
            _container
                .RegisterType<IPropertiesTableGenerator, PropertiesTableGenerator>(new ContainerControlledLifetimeManager())
                .RegisterType<IPropertiesTableGenerator, PropertiesTableGenerator>(new ContainerControlledLifetimeManager())
                .RegisterType<IBufferFactory, SegmentedBufferFactory>(new ContainerControlledLifetimeManager())
                .RegisterType(typeof(IImageFormatterFactoryProvider<>), typeof(ImageFormattersProvider<>), new ContainerControlledLifetimeManager());
        }
    }
}
