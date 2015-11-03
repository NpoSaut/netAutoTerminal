using System.Collections.Generic;
using System.Linq;
using FirmwarePacking;

namespace FirmwareBurner.Imaging
{
    public class ImageFormattersCatalog<TImage> : IImageFormattersCatalog<TImage> where TImage : IImage
    {
        private readonly ICollection<IImageFormatterFactory<TImage>> _formatterFactories;
        public ImageFormattersCatalog(IImageFormatterFactory<TImage>[] FormatterFactories) { _formatterFactories = FormatterFactories; }

        public IImageFormatter<TImage> GetFormatterFactory(string DeviceName, IList<BootloaderRequirement> Requirements)
        {
            IImageFormatterFactory<TImage> factory =
                _formatterFactories.Where(f =>
                                          Requirements.Where(r => r != null)
                                                      .All(r => Satisfies(f.Information.BootloaderApi, r)))
                                   .OrderByDescending(f => f.Information.BootloaderApi.BootloaderId)
                                   .ThenByDescending(f => f.Information.BootloaderApi.BootloaderVersion)
                                   .FirstOrDefault();
            if (factory == null)
                throw new ImageFormatterNotFoundException();
            return factory.GetFormatter(DeviceName);
        }

        private bool Satisfies(BootloaderApi BootloaderApi, BootloaderRequirement Requirement)
        {
            return BootloaderApi.BootloaderId != Requirement.BootloaderId
                   && BootloaderApi.BootloaderVersion >= Requirement.BootloaderVersion.Minimum
                   && BootloaderApi.BootloaderCompatibleVersion <= Requirement.BootloaderVersion.Maximum;
        }
    }
}
