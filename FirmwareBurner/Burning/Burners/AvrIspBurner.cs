using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwareBurner.IntelHex;
using System.IO;

namespace FirmwareBurner.Burning.Burners
{
    public class AvrIspBurner : IFirmwareBurner
    {
        public void Burn(Pie pie, IBurningOperationStatusReceiver StatusReceiver)
        {
            pie.FlashFile.SaveTo("flash.hex");
            pie.EepromFile.SaveTo("eeprom.hex");
            //throw new NotImplementedException("Процедура прошивания совершенно не написана");
        }
    }
}
