using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources.LoadControllers
{
    public interface IFirmwarePackageViewModelFactory
    {
        FirmwarePackageViewModel GetViewModel(string ElementKey, IRepositoryElement RepositoryElement, FirmwarePackageAvailabilityViewModel AvailabilityViewModel, ReleaseStatus Status);
    }
}