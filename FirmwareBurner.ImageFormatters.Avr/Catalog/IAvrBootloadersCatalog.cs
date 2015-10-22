using FirmwareBurner.ImageFormatters.Binary;

namespace FirmwareBurner.ImageFormatters.Avr.Catalog
{
    /// <summary>Каталог загрузчиков</summary>
    public interface IAvrBootloadersCatalog : IBootloaderInformationCatalog<AvrBootloaderInformation, AvrMemoryKind> { }
}
