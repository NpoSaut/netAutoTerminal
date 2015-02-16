using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources.LoadControllers
{
    public class FirmwarePackageViewModelFactory : IFirmwarePackageViewModelFactory
    {
        public FirmwarePackageViewModel GetViewModel(string ElementKey, IRepositoryElement RepositoryElement,
                                                     FirmwarePackageAvailabilityViewModel AvailabilityViewModel, ReleaseStatus Status)
        {
            var version = new FirmwareVersionViewModel(RepositoryElement.Information.FirmwareVersion.ToString(2),
                                                       RepositoryElement.Information.FirmwareVersionLabel,
                                                       RepositoryElement.Information.ReleaseDate);

            return new FirmwarePackageViewModel(ElementKey, version, AvailabilityViewModel, Status, RepositoryElement);
        }
    }
}
