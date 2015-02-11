namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class FirmwarePackageAvailabilityViewModelFactory
    {
        public FirmwarePackageAvailabilityViewModel GetAvailableViewModel() { return new FirmwarePackageAvailabilityViewModel(true); }
        public FirmwarePackageAvailabilityViewModel GetUnavailableViewModel() { return new FirmwarePackageAvailabilityViewModel(false); }

        public FirmwarePackageAvailabilityViewModel GetProgressViewModel(double Progress)
        {
            return new FirmwarePackageAvailabilityViewModel(false, true, Progress);
        }
    }
}