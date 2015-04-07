using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FirmwareBurner.IntelHex
{
    public class IntelHexStream : Stream
    {
        #region Параметры Stream

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush() { }
        public override int Read(byte[] buffer, int offset, int count) { throw new NotImplementedException(); }

        #endregion

        private long _length;
        public IntelHexStream() { Substreams = new List<SubStream>(); }
        private List<SubStream> Substreams { get; set; }

        public override long Length
        {
            get { return _length; }
        }

        public override long Position { get; set; }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position = Length + offset;
                    break;
            }
            return Position;
        }

        public override void SetLength(long value) { _length = value; }

        public override void Write(byte[] buffer, int offset, int count)
        {
            SubStream str = Substreams.SingleOrDefault(s => s.StartPosition <= Position && s.StartPosition + s.Length >= Position);
            if (str == null)
            {
                str = new SubStream(Position);
                Substreams.Add(str);
            }

            List<SubStream> overlappedStreams = Substreams.Where(s => s != str && s.StartPosition >= Position && s.StartPosition < Position + count).ToList();
            foreach (SubStream s in overlappedStreams)
            {
                str.Seek(s.StartPosition - str.StartPosition, SeekOrigin.Begin);
                s.Seek(0, SeekOrigin.Begin);
                s.CopyTo(str);
                Substreams.Remove(s);
            }

            str.Seek(Position - str.StartPosition, SeekOrigin.Begin);
            str.Write(buffer, offset, count);

            Position = str.StartPosition + str.Position;
        }

        public IntelHexFile GetHexFile()
        {
            return
                new IntelHexFile
                {
                    Substreams.SelectMany(ss => GetHexSegment(ss, (int)ss.StartPosition)),
                    new IntelHexEndLine()
                };
        }

        public String ToHexFormat() { return GetHexFile().ToHexFileString(); }

        public static IEnumerable<IntelHexLine> GetHexSegment(Byte[] Data, int StartAddress) { return GetHexSegment(new MemoryStream(Data), StartAddress); }

        public static IEnumerable<IntelHexLine> GetHexSegment(Stream Data, int StartAddress)
        {
            Int64 blockStartAddress = Int64.MinValue;
            Data.Seek(0, SeekOrigin.Begin);
            const int maxLength = 0x10;
            while (Data.Position != Data.Length)
            {
                int fullStartAddress = StartAddress + (int)Data.Position;
                if (fullStartAddress > blockStartAddress + UInt16.MaxValue)
                {
                    blockStartAddress = (fullStartAddress & 0xFFFF0000);
                    yield return new IntelHexExAddressLine((UInt16)(blockStartAddress >> 16));
                }

                var buff = new byte[maxLength];
                int len = Data.Read(buff, 0, maxLength);

                var line = new IntelHexDataLine((UInt16)(fullStartAddress - blockStartAddress), new MemoryStream(buff, 0, len));
                yield return line;
            }
        }

        public class SubStream : MemoryStream
        {
            public SubStream(long StartPosition) { this.StartPosition = StartPosition; }

            public SubStream(long StartPosition, int Length)
                : base(Length) { this.StartPosition = StartPosition; }

            public long StartPosition { get; set; }

            public override string ToString() { return string.Format("{0:X4} -- {1:X4}", StartPosition, StartPosition + Length); }
        }
    }
}
