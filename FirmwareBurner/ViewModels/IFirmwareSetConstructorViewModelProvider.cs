namespace FirmwareBurner.ViewModels
{
    public interface IFirmwareSetConstructorViewModelProvider
    {
        FirmwareSetConstructorViewModel GetViewModel(int CellKindId, int CellModificationId);
    }
}