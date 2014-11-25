﻿using FirmwareBurner.Imaging;
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
                .RegisterType<IPropertiesTableGenerator, PropertiesTableGenerator>(new ContainerControlledLifetimeManager());
        }
    }
}
