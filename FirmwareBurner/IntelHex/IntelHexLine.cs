using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FirmwareBurner.IntelHex
{
    public abstract class IntelHexLine
    {
        public abstract byte Key { get; }
        protected virtual UInt16 InternalAdress { get { return 0x0000; } }

        protected virtual Byte[] GetDataArray() { return new Byte[0]; }
        protected virtual Stream GetDataStream() { return new MemoryStream(GetDataArray()); }

        private Byte GetChecksum(IEnumerable<Byte> Content)
        {
            return (byte)(0xff - Content.Aggregate(0, (s, b) => s = unchecked(s + b)));
        }

        public String ToHexString()
        {
            var DataStream = GetDataStream();
            Byte[] buff = new Byte[DataStream.Length + 5];
            buff[0] = Key;
            buff[1] = (Byte)((InternalAdress & 0xff00) >> 8);
            buff[2] = (Byte)(InternalAdress & 0xff);
            DataStream.Seek(0, SeekOrigin.Begin);
            DataStream.Read(buff, 3, (int)DataStream.Length);
            buff[buff.Length - 1] = GetChecksum(buff);

            return string.Format(":{0}", string.Join("", buff.Select(b => b.ToString("X2"))));
        }
        public override string ToString()
        {
            return ToHexString();
        }
    }
}
