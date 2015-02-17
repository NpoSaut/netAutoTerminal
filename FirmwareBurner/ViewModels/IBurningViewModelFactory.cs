namespace FirmwareBurner.ViewModels
{
    public interface IBurningViewModelFactory
    {
        BurningViewModel GetViewModel(int CellKindId, int ModificationId, IProjectAssembler projectAssembler);
    }
}