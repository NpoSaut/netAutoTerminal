using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class FirmwarePackageAvailabilityViewModel : ViewModelBase
    {
        public FirmwarePackageAvailabilityViewModel(bool IsAvailable)
        {
            this.IsAvailable = IsAvailable;
            IsDownloading = false;
            DownloadingProgress = 0;
        }

        public FirmwarePackageAvailabilityViewModel(bool IsAvailable, bool IsDownloading, double DownloadingProgress)
        {
            this.IsAvailable = IsAvailable;
            this.IsDownloading = IsDownloading;
            this.DownloadingProgress = DownloadingProgress;
        }

        public bool IsAvailable { get; private set; }
        public bool IsDownloading { get; private set; }
        public double DownloadingProgress { get; private set; }
    }
}