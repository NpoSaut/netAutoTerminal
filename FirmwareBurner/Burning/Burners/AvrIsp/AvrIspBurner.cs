﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwareBurner.IntelHex;
using System.IO;
using FirmwareBurner.Burning.Burners.AvrIsp.stk500;

namespace FirmwareBurner.Burning.Burners.AvrIsp
{
    public class AvrIspBurner : IFirmwareBurner
    {
        private IAvrIspCommandShell Shell { get; set; }

        public AvrIspBurner(IAvrIspCommandShell Shell)
        {
            this.Shell = Shell;
        }

        public void Burn(Pie pie, IBurningOperationStatusReceiver StatusReceiver)
        {
            Shell.ChipName = "AT90CAN128";
            var tempFlashFile = new FileInfo(Path.GetTempFileName());
            pie.FlashFile.SaveTo(tempFlashFile);
            Shell.WriteFlash(tempFlashFile);
            tempFlashFile.Delete();
        }
    }
}
