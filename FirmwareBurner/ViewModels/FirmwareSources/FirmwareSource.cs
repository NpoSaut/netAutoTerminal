﻿using System;
using System.Linq;
using FirmwareBurner.ViewModels.Bases;
using FirmwarePacking;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public abstract class FirmwareSource : ViewModelBase
    {
        private bool _Ok;
        private FirmwarePackage _SelectedFirmware;

        public FirmwarePackage SelectedPackage
        {
            get { return _SelectedFirmware; }
            set
            {
                if (_SelectedFirmware != value)
                {
                    _SelectedFirmware = value;
                    OnPropertyChanged("SelectedPackage");
                    if (PackageSelected != null) PackageSelected(this, new EventArgs());
                }
            }
        }

        public bool Ok
        {
            get { return _Ok; }
            set
            {
                if (_Ok != value)
                {
                    _Ok = value;
                    OnPropertyChanged("Ok");
                }
            }
        }

        public event EventHandler PackageSelected;

        protected virtual void OnCheckTarget(ComponentTarget target) { }

        public void CheckTarget(ComponentTarget target)
        {
            OnCheckTarget(target);
            Ok = SelectedPackage != null && SelectedPackage.Components.Any(c => c.Targets.Any(cTarget => cTarget == target));
        }
    }
}
