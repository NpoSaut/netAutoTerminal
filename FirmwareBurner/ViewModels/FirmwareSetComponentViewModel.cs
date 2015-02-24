using System;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.FirmwareSources;

namespace FirmwareBurner.ViewModels
{
    public class FirmwareSetComponentViewModel : ViewModelBase
    {
        public FirmwareSetComponentViewModel(int ModuleIndex, string ModuleName, FirmwareSelectorViewModel FirmwareSelector)
        {
            this.FirmwareSelector = FirmwareSelector;
            this.ModuleName = ModuleName;
            this.ModuleIndex = ModuleIndex;

            FirmwareSelector.SelectedPackageChanged += FirmwareSelectorOnSelectedPackageChanged;
        }

        public int ModuleIndex { get; private set; }
        public String ModuleName { get; private set; }
        public FirmwareSelectorViewModel FirmwareSelector { get; private set; }
        public FirmwarePackageViewModel SelectedFirmware { get; private set; }
        public event EventHandler SelectedFirmwareChanged;

        private void FirmwareSelectorOnSelectedPackageChanged(object Sender, EventArgs Args)
        {
            SelectedFirmware = FirmwareSelector.SelectedPackage;
            OnSelectedFirmwareChanged();
        }

        protected virtual void OnSelectedFirmwareChanged()
        {
            RaisePropertyChanged("SelectedFirmware");
            EventHandler handler = SelectedFirmwareChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
