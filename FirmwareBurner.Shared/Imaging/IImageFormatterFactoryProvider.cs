using System.Collections.Generic;
using FirmwarePacking;

namespace FirmwareBurner.Imaging
{
    public interface IImageFormatterFactoryProvider<out TImage> where TImage : IImage
    {
        IImageFormatterFactory<TImage> GetFormatterFactory(string DeviceName, int Cell, int Modification, IList<BootloaderRequirement> Requirements);
    }
}
