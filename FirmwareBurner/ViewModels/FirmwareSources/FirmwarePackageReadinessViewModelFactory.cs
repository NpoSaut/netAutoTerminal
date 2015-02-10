namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class FirmwarePackageReadinessViewModelFactory
    {
        public FirmwarePackageAvailabilityViewModel GetAvailableViewModel() { return new FirmwarePackageAvailabilityViewModel(true); }
        public FirmwarePackageAvailabilityViewModel GetUnavailableViewModel() { return new FirmwarePackageAvailabilityViewModel(false); }

        public FirmwarePackageAvailabilityViewModel GetProgressViewModel(double Progress)
        {
            return new FirmwarePackageAvailabilityViewModel(false, true, Progress);
        }
    }
}