using System;
using System.Linq;

namespace FirmwareBurner.IntelHex
{
    internal class IntelHexExAddressLine : IntelHexLine
    {
        public IntelHexExAddressLine(UInt16 AddressExtension) { this.AddressExtension = AddressExtension; }

        public override byte Key
        {
            get { return 0x04; }
        }

        public UInt16 AddressExtension { get; set; }

        protected override byte[] GetDataArray() { return BitConverter.GetBytes(AddressExtension).Reverse().ToArray(); }

        public override string ToString() { return string.Format("[{0}] SET ADDR  {1:X4} : {2:X4}", Key, InternalAddress, AddressExtension); }
    }
}
