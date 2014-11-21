using System;

namespace FirmwareBurner.Imaging
{
    public interface IBinaryImageBuilder<out TImage> where TImage : IBinaryImage
    {
        TImage GetResult();
        void Write(String BufferName, int Offset, Byte[] Data);
    }
}
