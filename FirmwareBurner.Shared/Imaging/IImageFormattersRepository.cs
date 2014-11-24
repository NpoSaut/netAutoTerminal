using System;
using System.Collections.Generic;

namespace FirmwareBurner.Imaging
{
    public interface IImageFormattersRepository
    {
        IEnumerable<IImageFormatterFactory<TImage>> GetFormatterFactories<TImage>(String DeviceName) where TImage : IImage;
    }
}
