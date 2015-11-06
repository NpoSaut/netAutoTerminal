using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Avr
{
    public class AvrImageFormatterCatalog : EnumerableImageFormattersCatalogBase<AvrImage>
    {
        private IAvrBootloadersCatalog _bootloadersCatalog;
        public AvrImageFormatterCatalog(IAvrBootloadersCatalog BootloadersCatalog) { _bootloadersCatalog = BootloadersCatalog; }

        protected override IEnumerable<Fac> EnumerateFactories() { return Enumerable.Empty<Fac>(); }
    }
}
