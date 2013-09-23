using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwareBurner.IntelHex
{
    public class IntelHexExAdressLine : IntelHexLine
    {
        public override byte Key { get { return 0x04; } }

        public UInt32 AdressExtension { get; set; }

        public IntelHexExAdressLine(UInt32 AdressExtension)
        {
            this.AdressExtension = AdressExtension;
        }
    }
}
