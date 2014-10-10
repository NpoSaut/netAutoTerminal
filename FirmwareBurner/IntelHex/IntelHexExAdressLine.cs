using System;
using System.Linq;

namespace FirmwareBurner.IntelHex
{
    public class IntelHexExAdressLine : IntelHexLine
    {
        public IntelHexExAdressLine(UInt16 AdressExtension) { this.AdressExtension = AdressExtension; }

        public override byte Key
        {
            get { return 0x04; }
        }

        public UInt16 AdressExtension { get; set; }

        protected override byte[] GetDataArray() { return BitConverter.GetBytes(AdressExtension).Reverse().ToArray(); }

        public override string ToString() { return string.Format("[{0}] SET ADDR  {1:X4} : {2:X4}", Key, InternalAdress, AdressExtension); }
    }
}
