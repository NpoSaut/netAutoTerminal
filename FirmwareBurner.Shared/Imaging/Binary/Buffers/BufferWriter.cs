using System;

namespace FirmwareBurner.Imaging.Binary.Buffers
{
    public class BufferWriter
    {
        private readonly IBuffer _buffer;

        public BufferWriter(IBuffer Buffer, int Position = 0)
        {
            _buffer = Buffer;
            this.Position = Position;
        }

        private int Position { get; set; }

        public void WriteByte(Byte Data) { WriteBytes(Data); }

        public void WriteUInt32(UInt32 Data) { WriteBytes(BitConverter.GetBytes(Data)); }

        public void WriteInt32(Int32 Data) { WriteBytes(BitConverter.GetBytes(Data)); }

        public void WriteUInt16(UInt16 Data) { WriteBytes(BitConverter.GetBytes(Data)); }

        public void WriteInt16(Int16 Data) { WriteBytes(BitConverter.GetBytes(Data)); }

        public void WriteBytes(params byte[] Bytes)
        {
            _buffer.Write(Position, Bytes);
            Position += Bytes.Length;
        }
    }
}
