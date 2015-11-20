using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FirmwareBurner.Annotations;
using FirmwareBurner.BootloadersCatalog;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Cortex.Catalog
{
    [UsedImplicitly]
    public class CortexBootloaderInformationCatalog : ICortexBootloaderInformationCatalog
    {
        private readonly ICollection<CortexBootloaderInformation> _catalog;

        public CortexBootloaderInformationCatalog(IBootloadersCatalog BootloadersCatalog)
        {
            _catalog = BootloadersCatalog.GetBootloaders("CortexBootloader")
                                         .Select(CreateInformationInstance)
                                         .ToList();
        }

        public IEnumerable<CortexBootloaderInformation> GetBootloaderInformations() { return _catalog; }

        private CortexBootloaderInformation CreateInformationInstance(BootloaderCatalogRecord CatalogRecord)
        {
            return new CortexBootloaderInformation(
                CatalogRecord.TargetDevice,
                new BootloaderApi(CatalogRecord.Id, CatalogRecord.Version, CatalogRecord.CompatibleVersion),
                new Placement<CortexMemoryKind>(CortexMemoryKind.Flash, int.Parse(CatalogRecord.Properties["StaticPropertiesAddress"], NumberStyles.HexNumber)),
                new Placement<CortexMemoryKind>(CortexMemoryKind.Flash, int.Parse(CatalogRecord.Properties["DynamicPropertiesAddress"], NumberStyles.HexNumber)),
                new Placement<CortexMemoryKind>(CortexMemoryKind.Flash, int.Parse(CatalogRecord.Properties["BootloaderAddress"], NumberStyles.HexNumber)),
                new FileBodyLoader(CatalogRecord.FileName));
        }
    }
}
