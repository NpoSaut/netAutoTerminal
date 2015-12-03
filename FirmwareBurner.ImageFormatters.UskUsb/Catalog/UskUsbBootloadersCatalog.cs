using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FirmwareBurner.BootloadersCatalog;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;
using FirmwareBurner.ImageFormatters.Cortex;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.UskUsb.Catalog
{
    internal class UskUsbBootloadersCatalog : IUskUsbBootloadersCatalog
    {
        private readonly ICollection<UskUsbBootloaderInformation> _catalog;

        public UskUsbBootloadersCatalog(IBootloadersCatalog Catalog)
        {
            _catalog = Catalog.GetBootloaders("UskUsbBootloader")
                              .Select(GetBootloaderInformation)
                              .ToList();
        }

        public IEnumerable<UskUsbBootloaderInformation> GetBootloaderInformations() { return _catalog; }

        private UskUsbBootloaderInformation GetBootloaderInformation(BootloaderCatalogRecord CatalogRecord)
        {
            return new UskUsbBootloaderInformation(CatalogRecord.TargetDevice,
                                                   new Placement<CortexMemoryKind>(CortexMemoryKind.Flash,
                                                                                   Int32.Parse(CatalogRecord.Properties["BootloaderAddress"],
                                                                                               NumberStyles.HexNumber)),
                                                   new Placement<CortexMemoryKind>(CortexMemoryKind.Flash,
                                                                                   Int32.Parse(CatalogRecord.Properties["FileTableAddres"],
                                                                                               NumberStyles.HexNumber)),
                                                   new Placement<CortexMemoryKind>(CortexMemoryKind.Flash,
                                                                                   Int32.Parse(CatalogRecord.Properties["PropertiesTableAddress"],
                                                                                               NumberStyles.HexNumber)),
                                                   new FileBodyLoader(CatalogRecord.FileName),
                                                   new BootloaderApi(CatalogRecord.Id, CatalogRecord.Version, CatalogRecord.CompatibleVersion));
        }
    }
}
