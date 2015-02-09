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
        public FirmwarePackageViewModel(FirmwareVersionViewModel Version, bool LocallyAvailable)
        {
            this.LocallyAvailable = LocallyAvailable;
            this.Version = Version;
        }

        public bool LocallyAvailable { get; private set; }
        public FirmwareVersionViewModel Version { get; private set; }
    }
}
