namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public interface IFirmwareSelectorViewModelProvider
    {
        FirmwareSelectorViewModel GetViewModel(int CellKindId, int ModificationId);
    }

    public class CompositeFirmwareSelectorViewModelProvider : IFirmwareSelectorViewModelProvider
    {
        public FirmwareSelectorViewModel GetViewModel(int CellKindId, int ModificationId)
        {
            return new CompositeFirmwareSelectorViewModel(
                new FirmwareSelectorViewModel[]
                {
                    new ManualFirmwareSelectorViewModel("Из файла")
                });
        }
    }
}
