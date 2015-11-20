using System;

namespace FirmwareBurner.Imaging.Binary.Buffers
{
    public interface IWriter
    {
        void WriteByte(Byte Data);
        void WriteUInt32(UInt32 Data);
        void WriteInt32(Int32 Data);
        void WriteUInt16(UInt16 Data);
        void WriteInt16(Int16 Data);
        void WriteBytes(params byte[] Bytes);
    }
}