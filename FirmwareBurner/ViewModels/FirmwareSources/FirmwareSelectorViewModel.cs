using System;
using FirmwareBurner.ViewModels.Bases;
using FirmwarePacking;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public abstract class FirmwareSelectorViewModel : ViewModelBase
    {
        protected FirmwareSelectorViewModel(string Name) { this.Name = Name; }
        public String Name { get; private set; }

        public bool IsPackageSelected
        {
            get { return SelectedPackage != null; }
        }

        public FirmwareVersionViewModel SelectedVersion
        {
            get
            {
                if (!IsPackageSelected) return null;
                return new FirmwareVersionViewModel(SelectedPackage.Information.FirmwareVersion.ToString(2), SelectedPackage.Information.FirmwareVersionLabel,
                                                    SelectedPackage.Information.ReleaseDate);
            }
        }

        public abstract FirmwarePackage SelectedPackage { get; }
    }
}
