using System.Collections.Generic;
using FirmwarePacking;

namespace FirmwareBurner.Imaging
{
    public interface IImageFormattersCatalog<out TImage> where TImage : IImage
    {
        IImageFormatter<TImage> GetFormatterFactory(string DeviceName, IList<BootloaderRequirement> Requirements);
    }
}
