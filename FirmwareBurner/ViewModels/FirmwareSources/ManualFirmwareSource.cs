using System;
using FirmwarePacking;
using Microsoft.Win32;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class ManualFirmwareSource : FirmwareSource
    {
        public void OpenFirmware()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
                SelectedPackage = FirmwarePackage.Open(dlg.FileName);
        }

        protected override void OnCheckTarget(ComponentTarget target) { throw new NotImplementedException(); }
    }
}
