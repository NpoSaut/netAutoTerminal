using FirmwareBurner.Annotations;
using FirmwareBurner.Validation;
using FirmwareBurner.ViewModels.Property;
using FirmwareBurner.ViewModels.Targeting;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public interface IProjectViewModelProvider
    {
        ProjectViewModel GetViewModel(int CellKindId, int CellModificationId, IValidationContext ValidationContext);
    }

    [UsedImplicitly]
    public class ProjectViewModelProvider : IProjectViewModelProvider
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFirmwareSetConstructorViewModelProvider _firmwareSetConstructorViewModelProvider;

        public ProjectViewModelProvider(IFirmwareSetConstructorViewModelProvider FirmwareSetConstructorViewModelProvider, IEventAggregator EventAggregator)
        {
            _firmwareSetConstructorViewModelProvider = FirmwareSetConstructorViewModelProvider;
            _eventAggregator = EventAggregator;
        }

        public ProjectViewModel GetViewModel(int CellKindId, int CellModificationId, IValidationContext ValidationContext)
        {
            var project = new ProjectViewModel(CellKindId, CellModificationId,
                                               new BlockDetailsViewModel(),
                                               _firmwareSetConstructorViewModelProvider.GetViewModel(CellKindId, CellModificationId),
                                               _eventAggregator);

            RegisterValidateableProperties(project, ValidationContext);

            return project;
        }

        private static void RegisterValidateableProperties(ProjectViewModel project, IValidationContext ValidationContext)
        {
            if (ValidationContext == null) return;
            ValidationContext.RegisterValidateableElement(project.BlockDetails.AssemblyDate);
            ValidationContext.RegisterValidateableElement(project.BlockDetails.SerialNumber);
            foreach (ValidateableFirmwareSetComponentViewModel component in project.FirmwareSetConstructor.Components)
                ValidationContext.RegisterValidateableElement(component);
        }
    }
}
