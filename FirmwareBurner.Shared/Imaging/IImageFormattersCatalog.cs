using System.Collections.Generic;
using FirmwarePacking;

namespace FirmwareBurner.Imaging
{
    public interface IImageFormattersCatalog<out TImage> where TImage : IImage
    {
        IEnumerable<IImageFormatterFactory<TImage>> GetAppropriateFormatterFactories(string DeviceName, IList<BootloaderRequirement> Requirements);
    }
}