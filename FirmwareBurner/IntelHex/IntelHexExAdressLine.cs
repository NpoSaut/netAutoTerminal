using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwareBurner.IntelHex
{
    public class IntelHexExAdressLine : IntelHexLine
    {
        public override byte Key { get { return 0x04; } }

        public UInt16 AdressExtension { get; set; }

        public IntelHexExAdressLine(UInt16 AdressExtension)
        {
            this.AdressExtension = AdressExtension;
        }

        protected override byte[] GetDataArray()
        {
            return BitConverter.GetBytes(AdressExtension).Reverse().ToArray();
        }

        public override string ToString()
        {
            return string.Format("[{0}] SET ADDR  {1:X4} : {2:X4}", Key, InternalAdress, AdressExtension);
        }
    }
}
