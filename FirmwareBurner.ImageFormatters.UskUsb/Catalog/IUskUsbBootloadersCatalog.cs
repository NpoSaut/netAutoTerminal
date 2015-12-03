using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Cortex;

namespace FirmwareBurner.ImageFormatters.UskUsb.Catalog
{
    public interface IUskUsbBootloadersCatalog : IBootloaderInformationCatalog<UskUsbBootloaderInformation, CortexMemoryKind> { }
}
