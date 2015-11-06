using System.Collections.Generic;

namespace FirmwareBurner.ImageFormatters.Binary
{
    public interface IBootloaderInformationCatalog<out TBootloaderInformation, TMemoryKind>
        where TBootloaderInformation : BinaryBootloaderInformation<TMemoryKind>
    {
        IEnumerable<TBootloaderInformation> GetBootloaderInformations();
    }
}
