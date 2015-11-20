using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FirmwareBurner.Annotations;
using FirmwareBurner.BootloadersCatalog;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Avr.Catalog
{
    /// <summary>Захардкоженый каталог загрузчиков</summary>
    [UsedImplicitly]
    public class AvrBootloadersCatalog : IAvrBootloadersCatalog
    {
        private readonly ICollection<AvrBootloaderInformation> _catalog;

        public AvrBootloadersCatalog(IBootloadersCatalog Catalog)
        {
            _catalog = Catalog.GetBootloaders("AvrBootloader")
                              .Select(GetDimaAvrBootloaderInformation)
                              .ToList();
        }

        public IEnumerable<AvrBootloaderInformation> GetBootloaderInformations() { return _catalog; }

        private static AvrBootloaderInformation GetDimaAvrBootloaderInformation(BootloaderCatalogRecord CatalogRecord)
        {
            int bootloaderAddress = int.Parse(CatalogRecord.Properties["BootloaderAddress"], NumberStyles.HexNumber);
            return new AvrBootloaderInformation(CatalogRecord.TargetDevice,
                                                new BootloaderApi(CatalogRecord.Id,
                                                                  CatalogRecord.Version,
                                                                  CatalogRecord.CompatibleVersion),
                                                new AvrFuses(byte.Parse(CatalogRecord.Properties["FuseH"], NumberStyles.HexNumber),
                                                             byte.Parse(CatalogRecord.Properties["FuseL"], NumberStyles.HexNumber),
                                                             byte.Parse(CatalogRecord.Properties["FuseX"], NumberStyles.HexNumber)),
                                                new FileBodyLoader(CatalogRecord.FileName), new Placement<AvrMemoryKind>(AvrMemoryKind.Flash, bootloaderAddress),
                                                new Placement<AvrMemoryKind>(AvrMemoryKind.Flash, bootloaderAddress - 0x200),
                                                new Placement<AvrMemoryKind>(AvrMemoryKind.Flash, bootloaderAddress - 0x100));
        }
    }
}
