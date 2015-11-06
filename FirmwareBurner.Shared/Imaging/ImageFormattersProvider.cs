using System.Collections.Generic;
using System.Linq;
using FirmwarePacking;

namespace FirmwareBurner.Imaging
{
    public class ImageFormattersProvider<TImage> : IImageFormatterFactoryProvider<TImage> where TImage : IImage
    {
        private readonly ICollection<IImageFormattersCatalog<TImage>> _formattersCatalogs;
        public ImageFormattersProvider(IImageFormattersCatalog<TImage>[] FormattersCatalogs) { _formattersCatalogs = FormattersCatalogs; }

        public IImageFormatterFactory<TImage> GetFormatterFactory(string DeviceName, IList<BootloaderRequirement> Requirements)
        {
            IImageFormatterFactory<TImage> appropriatedFormatterFactory =
                _formattersCatalogs.SelectMany(catalog => catalog.GetAppropriateFormatterFactories(DeviceName, Requirements))
                                   .OrderByDescending(f => f.Information.BootloaderApi.BootloaderId)
                                   .ThenByDescending(f => f.Information.BootloaderApi.BootloaderVersion)
                                   .ToList()
                                   .FirstOrDefault();
            if (appropriatedFormatterFactory == null)
                throw new ImageFormatterNotFoundException();
            return appropriatedFormatterFactory;
        }
    }
}
