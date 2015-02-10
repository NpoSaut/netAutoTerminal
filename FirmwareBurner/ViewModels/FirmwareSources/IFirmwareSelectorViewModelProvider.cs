namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public interface IFirmwareSelectorViewModelProvider
    {
        FirmwareSelectorViewModel GetViewModel(int CellKindId, int ModificationId, int ModuleId, int ChannelsCount);
    }
}
