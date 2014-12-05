using System;
using System.Collections.Generic;
using System.Linq;
using FirmwarePacking;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class CompositeFirmwareSelectorViewModel : FirmwareSelectorViewModel
    {
        private FirmwareSelectorViewModel _selectedSelector;

        public CompositeFirmwareSelectorViewModel(IList<FirmwareSelectorViewModel> Children) : base("composite")
        {
            this.Children = Children;
            SelectedSelector = Children.FirstOrDefault();
        }

        public FirmwareSelectorViewModel SelectedSelector
        {
            get { return _selectedSelector; }
            set
            {
                if (_selectedSelector != value)
                {
                    if (_selectedSelector != null)
                        _selectedSelector.SelectedPackageChanged -= SelectedSelectorOnSelectedPackageChanged;
                    _selectedSelector = value;
                    _selectedSelector.SelectedPackageChanged += SelectedSelectorOnSelectedPackageChanged;
                    RaisePropertyChanged("SelectedSelector");
                    OnSelectedPackageChanged();
                }
            }
        }

        public IList<FirmwareSelectorViewModel> Children { get; private set; }

        public override FirmwarePackage SelectedPackage
        {
            get
            {
                if (SelectedSelector == null) return null;
                return SelectedSelector.SelectedPackage;
            }
        }

        private void SelectedSelectorOnSelectedPackageChanged(object Sender, EventArgs EventArgs) { OnSelectedPackageChanged(); }
    }
}
