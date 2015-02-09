using System.Collections.Generic;
using System.Collections.ObjectModel;
using FirmwareBurner.Annotations;
using FirmwareBurner.ViewModels.Bases;
using FirmwarePacking;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class IntegratedFirmwareSelectorViewModel : FirmwareSelectorViewModel
    {
        private ObservableCollection<FirmwarePackageViewModel> _packages;

        public IntegratedFirmwareSelectorViewModel([NotNull] IList<FirmwarePackageViewModel> Packages) : base("Интегрированный")
        {
            _packages = new ObservableCollection<FirmwarePackageViewModel>(Packages);
            this.Packages = new ReadOnlyObservableCollection<FirmwarePackageViewModel>(_packages);
        }

        public ReadOnlyObservableCollection<FirmwarePackageViewModel> Packages { get; private set; }

        public override FirmwarePackage SelectedPackage
        {
            get { return null; }
        }
    }

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

    public class FirmwarePackageReadinessViewModelFactory
    {
        public FirmwarePackageAvailabilityViewModel GetAvailableViewModel() { return new FirmwarePackageAvailabilityViewModel(true); }
        public FirmwarePackageAvailabilityViewModel GetUnavailableViewModel() { return new FirmwarePackageAvailabilityViewModel(false); }
        public FirmwarePackageAvailabilityViewModel GetProgressViewModel(double Progress) { return new FirmwarePackageAvailabilityViewModel(false, true, Progress); }
    }
}
