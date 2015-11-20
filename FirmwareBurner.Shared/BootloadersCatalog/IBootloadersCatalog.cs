using System.Collections.Generic;

namespace FirmwareBurner.BootloadersCatalog
{
    public interface IBootloadersCatalog
    {
        IEnumerable<BootloaderCatalogRecord> GetBootloaders(string BootloaderKind);
    }
}
