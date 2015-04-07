using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FirmwareBurner.IntelHex
{
    public abstract class IntelHexLine
    {
        public abstract byte Key { get; }

        protected virtual UInt16 InternalAddress
        {
            get { return 0x0000; }
        }

        protected virtual Byte[] GetDataArray() { return new Byte[0]; }
        protected virtual Stream GetDataStream() { return new MemoryStream(GetDataArray()); }

        private Byte GetChecksum(IEnumerable<Byte> Content) { return (byte)(0x100 - Content.Aggregate(0, (s, b) => unchecked(s + b))); }

        public String ToHexString()
        {
            Stream dataStream = GetDataStream();
            var buff = new Byte[dataStream.Length + 5];
            buff[0] = (Byte)dataStream.Length;
            buff[1] = (Byte)((InternalAddress & 0xff00) >> 8);
            buff[2] = (Byte)(InternalAddress & 0xff);
            buff[3] = Key;
            dataStream.Seek(0, SeekOrigin.Begin);
            dataStream.Read(buff, 4, (int)dataStream.Length);
            buff[buff.Length - 1] = GetChecksum(buff);

            return string.Format(":{0}", string.Join("", buff.Select(b => b.ToString("X2"))));
        }

        public override string ToString() { return ToHexString(); }
    }
}
