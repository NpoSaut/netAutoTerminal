using System;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.Validation
{
    public class ValidationModule : IModule
    {
        private readonly IUnityContainer _container;
        public ValidationModule(IUnityContainer Container) { _container = Container; }

        /// <summary>Notifies the module that it has be initialized.</summary>
        public void Initialize()
        {
            _container
                .RegisterType<IProjectValidator, RulesListProjectValidator>(new ContainerControlledLifetimeManager());

            foreach (Type ruleType in Assembly.GetAssembly(GetType()).GetTypes().Where(t => t.GetInterfaces().Contains(typeof (IProjectValidationRule))))
                _container.RegisterType(typeof (IProjectValidationRule), ruleType, ruleType.FullName, new ContainerControlledLifetimeManager());
        }
    }
}
