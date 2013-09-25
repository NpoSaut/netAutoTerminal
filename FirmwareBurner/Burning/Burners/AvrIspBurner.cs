using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwareBurner.IntelHex;

namespace FirmwareBurner.Burning.Burners
{
    public class AvrIspBurner : IFirmwareBurner
    {
        public void Burn(Pie pie, IBurningOperationStatusReceiver StatusReceiver)
        {
            throw new NotImplementedException("Процедура прошивания совершенно не написана");
        }
    }
}
