using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwarePacking;

namespace FirmwareBurner.Models.FirmwareSources
{
    public abstract class FirmwareSource : ViewModelBase
    {
        public event EventHandler PackageSelected;

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

        private bool _Ok;
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

        protected virtual void OnCheckTarget(ComponentTarget target) { }

        public void CheckTarget(ComponentTarget target)
        {
            OnCheckTarget(target);
            Ok = SelectedPackage != null && SelectedPackage.Components.Any(c => c.Targets.Any(cTarget => cTarget == target));
        }
    }

}