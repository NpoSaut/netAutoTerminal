using System;

namespace FirmwareBurner.Imaging
{
    public interface IImageFormatterFactory<out TImage> where TImage : IImage
    {
        IImageFormatter<TImage> GetFormatter(String DeviceName);
    }
}
