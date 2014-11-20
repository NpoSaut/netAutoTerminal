using System;
using FirmwareBurner.ViewModels.Bases;
using FirmwarePacking;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public abstract class FirmwareSelectorViewModel : ViewModelBase
    {
        protected FirmwareSelectorViewModel(string Name) { this.Name = Name; }
        public String Name { get; private set; }

        public bool IsPackageSelected { get; private set; }
        public FirmwareVersionViewModel SelectedVersion { get; private set; }
        public FirmwarePackage SelectedPackage { get; private set; }

        protected void SelectPackage(FirmwarePackage Package)
        {
            IsPackageSelected = Package != null;
            SelectedPackage = Package;
            SelectedVersion = Package != null
                                  ? new FirmwareVersionViewModel(Package.Information.FirmwareVersion.ToString(2), Package.Information.FirmwareVersionLabel)
                                  : null;
            RaisePropertyChanged("SelectedPackage");
            RaisePropertyChanged("SelectedVersion");
            RaisePropertyChanged("IsPackageSelected");
        }
    }
}
