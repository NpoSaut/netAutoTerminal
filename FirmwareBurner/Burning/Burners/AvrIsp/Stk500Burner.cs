using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwareBurner.IntelHex;
using System.IO;
using FirmwareBurner.Burning.Burners.AvrIsp.stk500;

namespace FirmwareBurner.Burning.Burners.AvrIsp
{
    public class Stk500Burner : IFirmwareBurner
    {
        public void Burn(Pie pie, IBurningOperationStatusReceiver StatusReceiver)
        {
            pie.FlashFile.SaveTo("flash.hex");
            pie.EepromFile.SaveTo("eeprom.hex");

            var b = new Stk500("AT90CAN128");
            //var sig = b.GetSignature();
            b.WriteFlash(new FileInfo("flash.hex"));

            //throw new NotImplementedException("Процедура прошивания совершенно не написана");
        }
    }
}
