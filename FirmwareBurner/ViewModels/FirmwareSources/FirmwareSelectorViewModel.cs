using System;
using FirmwareBurner.ViewModels.Bases;

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
            get { return IsPackageSelected ? SelectedPackage.Version : null; }
        }

        public abstract FirmwarePackageViewModel SelectedPackage { get; set; }
        public event EventHandler SelectedPackageChanged;

        protected virtual void OnSelectedPackageChanged()
        {
            RaisePropertyChanged("SelectedPackage");
            RaisePropertyChanged("SelectedVersion");
            RaisePropertyChanged("IsPackageSelected");
            EventHandler handler = SelectedPackageChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
