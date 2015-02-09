using FirmwareBurner.Annotations;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public interface IFirmwareSelectorViewModelProvider
    {
        FirmwareSelectorViewModel GetViewModel(int CellKindId, int ModificationId, int ModuleId, int ChannelsCount);
    }

    [UsedImplicitly]
    public class IntegratedFirmwareSelectorViewModelProvider : IFirmwareSelectorViewModelProvider
    {
        public FirmwareSelectorViewModel GetViewModel(int CellKindId, int ModificationId, int ModuleId, int ChannelsCount)
        {
            return new IntegratedFirmwareSelectorViewModel(new FirmwarePackageViewModel[0]);
        }
    }
}
