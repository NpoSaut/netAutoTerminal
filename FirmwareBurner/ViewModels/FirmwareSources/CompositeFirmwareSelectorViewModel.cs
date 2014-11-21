﻿using System.Collections.Generic;
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
                    _selectedSelector = value;
                    RaisePropertyChanged("SelectedSelector");
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
    }
}