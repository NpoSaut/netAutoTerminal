using FirmwareBurner.Validation;

namespace FirmwareBurner.ViewModels
{
    public interface IBurningViewModelFactory
    {
        BurningViewModel GetViewModel(int CellKindId, int ModificationId, IValidationContext ValidationContext, IProjectAssembler projectAssembler);
    }
}