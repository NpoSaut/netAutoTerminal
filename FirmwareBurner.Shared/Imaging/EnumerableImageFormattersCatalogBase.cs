using System.Collections.Generic;
using System.Linq;
using FirmwarePacking;

namespace FirmwareBurner.Imaging
{
    public abstract class EnumerableImageFormattersCatalogBase<TImage> : IImageFormattersCatalog<TImage> where TImage : IImage
    {
        public IEnumerable<IImageFormatterFactory<TImage>> GetAppropriateFormatterFactories(string DeviceName, IList<BootloaderRequirement> Requirements)
        {
            return
                EnumerateFactories().Where(f => f.DeviceName == DeviceName)
                                    .Where(f => Requirements.Where(r => r != null)
                                                            .All(r => Satisfies(f.Factory.Information.BootloaderApi, r)))
                                    .Select(f => f.Factory);
        }

        protected abstract IEnumerable<Fac> EnumerateFactories();

        private bool Satisfies(BootloaderApi BootloaderApi, BootloaderRequirement Requirement)
        {
            return BootloaderApi.BootloaderId != Requirement.BootloaderId
                   && BootloaderApi.BootloaderVersion >= Requirement.BootloaderVersion.Minimum
                   && BootloaderApi.BootloaderCompatibleVersion <= Requirement.BootloaderVersion.Maximum;
        }

        protected class Fac
        {
            public Fac(string DeviceName, IImageFormatterFactory<TImage> Factory)
            {
                this.DeviceName = DeviceName;
                this.Factory = Factory;
            }

            public string DeviceName { get; private set; }
            public IImageFormatterFactory<TImage> Factory { get; private set; }
        }
    }
}