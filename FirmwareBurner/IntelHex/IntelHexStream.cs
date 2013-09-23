using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FirmwareBurner.IntelHex
{
    public class IntelHexStream : Stream
    {
        #region Параметры Stream
        public override bool CanRead { get { return false; } }
        public override bool CanSeek { get { return true; } }
        public override bool CanWrite { get { return true; } }
        public override void Flush() { }
        public override int Read(byte[] buffer, int offset, int count) { throw new NotImplementedException(); } 
        #endregion

        public class SubStream : MemoryStream
        {
            public long StartPosition { get; set; }
            public SubStream(long StartPosition)
                : base()
            {
                this.StartPosition = StartPosition;
            }
            public SubStream(long StartPosition, int Length)
                : base(Length)
            {
                this.StartPosition = StartPosition;
            }

            public override string ToString()
            {
                return string.Format("{0:X4} -- {1:X4}", StartPosition, StartPosition + Length);
            }
        }

        private List<SubStream> Substreams { get; set; }

        public IntelHexStream()
        {
            Substreams = new List<SubStream>();
        }

        private long _Length = 0;
        public override long Length
        {
            get { return _Length; }
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

        public override void SetLength(long value)
        {
            _Length = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            SubStream str = Substreams.SingleOrDefault(s => s.StartPosition <= Position && s.StartPosition + s.Length >= Position);
            if (str == null)
            {
                str = new SubStream(Position);
                Substreams.Add(str);
            }

            var overlappedStreams = Substreams.Where(s => s != str && s.StartPosition >= Position && s.StartPosition < Position + count).ToList();
            foreach (var s in overlappedStreams)
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
                new IntelHexFile()
                {
                    Substreams.SelectMany(ss => GetHexSegment(ss, (int)ss.StartPosition)),
                    new IntelHexEndLine()
                };
        }

        public String ToHexFormat()
        {
            return GetHexFile().ToHexFileString();
        }

        public static IEnumerable<IntelHexLine> GetHexSegment(Byte[] Data, int StartAdress) { return GetHexSegment(new MemoryStream(Data), StartAdress); }
        public static IEnumerable<IntelHexLine> GetHexSegment(Stream Data, int StartAdress)
        {
            int BlockStartAdress = int.MinValue;
            Data.Seek(0, SeekOrigin.Begin);
            const int MaxLength = byte.MaxValue;
            StringBuilder sb = new StringBuilder();
            while (Data.Position != Data.Length)
            {
                int FullStartAdress = StartAdress + (int)Data.Position;
                if (FullStartAdress > BlockStartAdress + UInt16.MaxValue)
                {
                    BlockStartAdress = (int)(FullStartAdress & 0xFFFF0000);
                    yield return new IntelHexExAdressLine((UInt16)BlockStartAdress);
                }

                byte[] buff = new byte[MaxLength];
                int len = Data.Read(buff, 0, MaxLength);

                var line = new IntelHexDataLine((UInt16)(FullStartAdress - BlockStartAdress), new MemoryStream(buff, 0, len));
                yield return line;
            }
        }


    }




}
