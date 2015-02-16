using FirmwareBurner.Annotations;
using FirmwareBurner.ViewModels.Bases;
using FirmwarePacking;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class FirmwarePackageViewModel : ViewModelBase
    {
        private readonly IFirmwarePackageProvider _packageProvider;
        private FirmwarePackageAvailabilityViewModel _availability;
        private ReleaseStatus _status;

        public FirmwarePackageViewModel(string Key, FirmwareVersionViewModel Version, FirmwarePackageAvailabilityViewModel Availability, ReleaseStatus Status,
                                        [NotNull] IFirmwarePackageProvider PackageProvider)
        {
            this.Key = Key;
            this.Status = Status;
            _packageProvider = PackageProvider;
            this.Availability = Availability;
            this.Version = Version;
        }

        public string Key { get; private set; }

        public FirmwarePackageAvailabilityViewModel Availability
        {
            get { return _availability; }
            set
            {
                if (Equals(value, _availability)) return;
                _availability = value;
                RaisePropertyChanged(() => Availability);
            }
        }

        public FirmwareVersionViewModel Version { get; private set; }

        public ReleaseStatus Status
        {
            get { return _status; }
            set
            {
                if (value == _status) return;
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        public FirmwarePackage GetPackageBody() { return _packageProvider.GetPackage(); }
    }
}
