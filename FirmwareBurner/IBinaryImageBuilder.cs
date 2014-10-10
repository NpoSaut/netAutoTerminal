using System;
using FirmwareBurner.Models.Images.Binary;

namespace FirmwareBurner
{
    public interface IBinaryImageBuilder<out TImage> where TImage : IBinaryImage
    {
        TImage GetResult();
        void Write(String BufferName, int Offset, Byte[] Data);
    }
}
