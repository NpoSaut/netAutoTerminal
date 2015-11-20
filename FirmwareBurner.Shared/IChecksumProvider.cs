using System;

namespace FirmwareBurner
{
    public interface IChecksumProvider
    {
        ushort GetChecksum(Byte[] Data);
    }
}