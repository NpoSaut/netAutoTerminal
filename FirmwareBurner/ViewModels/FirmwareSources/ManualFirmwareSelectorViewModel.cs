using System;
using System.Windows.Input;
using FirmwarePacking;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class ManualFirmwareSelectorViewModel : FirmwareSelectorViewModel
    {
        private string _firmwarePath;

        public ManualFirmwareSelectorViewModel(string Name) : base(Name)
        {
            OpenPackageCommand = new DelegateCommand(OpenPackage);
            FirmwarePath = "Выбрать файл";
        }

        public string FirmwarePath
        {
            get { return _firmwarePath; }
            set
            {
                if (_firmwarePath != value)
                {
                    _firmwarePath = value;
                    RaisePropertyChanged("FirmwarePath");
                }
            }
        }

        public ICommand OpenPackageCommand { get; private set; }

        public void OpenPackage()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
                SelectedPackage = FirmwarePackage.Open(dlg.FileName);
        }

        protected override void OnCheckTarget(ComponentTarget target) { throw new NotImplementedException(); }
    }
}
