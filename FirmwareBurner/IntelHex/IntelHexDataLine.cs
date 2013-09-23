using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FirmwareBurner.IntelHex
{
    public class IntelHexDataLine : IntelHexLine
    {
        public override byte Key { get { return 0x00; } }
        public UInt16 Adress { get; set; }
        protected override ushort InternalAdress
        {
            get { return Adress; }
        }
        public Stream Data { get; private set; }

        public IntelHexDataLine(UInt16 StartAdress, Byte[] DataBuffer)
            : this(StartAdress, new MemoryStream(DataBuffer))
        { }
        public IntelHexDataLine(UInt16 StartAdress, Stream DataStream)
        {
            this.Adress = StartAdress;
            this.Data = DataStream;
        }


        protected override Stream GetDataStream()
        {
            return Data;
        }
    }
}
