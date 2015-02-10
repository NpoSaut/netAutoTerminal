using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class FirmwarePackageViewModel : ViewModelBase
    {
        public FirmwarePackageViewModel(FirmwareVersionViewModel Version, FirmwarePackageAvailabilityViewModel Availability)
        {
            this.Availability = Availability;
            this.Version = Version;
        }

        public FirmwarePackageAvailabilityViewModel Availability { get; private set; }
        public FirmwareVersionViewModel Version { get; private set; }
    }
}