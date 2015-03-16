using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.Validation
{
    public class ValidationsModule : IModule
    {
        private readonly IUnityContainer _container;
        public ValidationsModule(IUnityContainer Container) { _container = Container; }

        /// <summary>Notifies the module that it has be initialized.</summary>
        public void Initialize()
        {
            _container
                .RegisterType<IValidationContextFactory, ValidationContextFactory>(new ContainerControlledLifetimeManager());
        }
    }
}
