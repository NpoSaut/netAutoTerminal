using System;
using System.IO;
using System.Linq;

namespace FirmwareBurner.IntelHex
{
    public class IntelHexDataLine : IntelHexLine
    {
        public IntelHexDataLine(UInt16 StartAddress, Byte[] DataBuffer)
            : this(StartAddress, new MemoryStream(DataBuffer)) { }

        public IntelHexDataLine(UInt16 StartAddress, Stream DataStream)
        {
            Address = StartAddress;
            Data = DataStream;
        }

        public override byte Key
        {
            get { return 0x00; }
        }

        public UInt16 Address { get; set; }

        protected override ushort InternalAddress
        {
            get { return Address; }
        }

        public Stream Data { get; private set; }

        protected override byte[] GetDataArray()
        {
            long p = Data.Position;
            var a = new Byte[Data.Length];
            Data.Read(a, 0, (int)Data.Length);
            Data.Seek(p, SeekOrigin.Begin);
            return a;
        }

        protected override Stream GetDataStream() { return Data; }

        public override string ToString()
        {
            return string.Format("[{0}] DATA      {1:X4} : {2}", Key, InternalAddress, string.Join(" ", GetDataArray().Select(b => b.ToString("X2"))));
        }
    }
}
