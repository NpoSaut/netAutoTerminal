using System;

namespace FirmwareBurner.Imaging.Binary.Buffers
{
    public abstract class WriterBase : IWriter
    {
        public void WriteByte(Byte Data) { WriteBytes(Data); }
        public void WriteUInt32(UInt32 Data) { WriteBytes(BitConverter.GetBytes(Data)); }
        public void WriteInt32(Int32 Data) { WriteBytes(BitConverter.GetBytes(Data)); }
        public void WriteUInt16(UInt16 Data) { WriteBytes(BitConverter.GetBytes(Data)); }
        public void WriteInt16(Int16 Data) { WriteBytes(BitConverter.GetBytes(Data)); }
        public abstract void WriteBytes(params byte[] Bytes);
    }
}