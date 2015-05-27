using System;
using System.Collections.Generic;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels
{
    public class BurningViewModel : ViewModelBase
    {
        private BurningMethodViewModel _selectedBurningMethod;

        public BurningViewModel(ICollection<BurningOptionViewModel> BurningOptions,
                                ICollection<BurningMethodViewModel> BurningMethods)
        {
            this.BurningOptions = BurningOptions;
            this.BurningMethods = BurningMethods;
        }

        public BurningMethodViewModel SelectedBurningMethod
        {
            get { return _selectedBurningMethod; }
            set
            {
                if (Equals(value, _selectedBurningMethod)) return;
                _selectedBurningMethod = value;
                if (SelectedBurningMethodChanged != null)
                    SelectedBurningMethodChanged(this, new EventArgs());
            }
        }

        public ICollection<BurningOptionViewModel> BurningOptions { get; private set; }
        public ICollection<BurningMethodViewModel> BurningMethods { get; private set; }
        public event EventHandler SelectedBurningMethodChanged;
    }
}
