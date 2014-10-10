using System;
using System.IO;
using System.Linq;

namespace FirmwareBurner.IntelHex
{
    public class IntelHexDataLine : IntelHexLine
    {
        public IntelHexDataLine(UInt16 StartAdress, Byte[] DataBuffer)
            : this(StartAdress, new MemoryStream(DataBuffer)) { }

        public IntelHexDataLine(UInt16 StartAdress, Stream DataStream)
        {
            Adress = StartAdress;
            Data = DataStream;
        }

        public override byte Key
        {
            get { return 0x00; }
        }

        public UInt16 Adress { get; set; }

        protected override ushort InternalAdress
        {
            get { return Adress; }
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
            return string.Format("[{0}] DATA      {1:X4} : {2}", Key, InternalAdress, string.Join(" ", GetDataArray().Select(b => b.ToString("X2"))));
        }
    }
}
