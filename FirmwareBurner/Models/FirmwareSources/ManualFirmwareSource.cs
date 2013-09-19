using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using FirmwarePacking;

namespace FirmwareBurner.Models.FirmwareSources
{
    public class ManualFirmwareSource : FirmwareSource
    {
        public void OpenFirmware()
        {
            var dlg = new OpenFileDialog() { };
            if (dlg.ShowDialog() == true)
            {
                SelectedPackage = FirmwarePackage.Open(dlg.FileName);
            }
        }

        protected override void OnCheckTarget(ComponentTarget target)
        {
            throw new NotImplementedException();
        }
    }
}
