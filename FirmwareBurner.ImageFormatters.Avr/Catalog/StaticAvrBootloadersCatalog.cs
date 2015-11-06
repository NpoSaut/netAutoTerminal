using System.Collections.Generic;
using System.Reflection;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;

namespace FirmwareBurner.ImageFormatters.Avr.Catalog
{
    /// <summary>Захардкоженый каталог загрузчиков</summary>
    public class StaticAvrBootloadersCatalog : IAvrBootloadersCatalog
    {
        private static readonly Assembly _assembly = Assembly.GetAssembly(typeof (StaticAvrBootloadersCatalog));
        private readonly ICollection<AvrBootloaderInformation> _catalog;

        public StaticAvrBootloadersCatalog()
        {
            _catalog =
                new[]
                {
                    GetDimaAvrBootloaderInformation("at90can128", 0x1e000, "Bootloaders.Bootloader_128k"),
                    GetDimaAvrBootloaderInformation("at90can64", 0x0e000, "Bootloaders.Bootloader_64k")
                };
        }

        /// <summary>Волшебные FUSE-биты</summary>
        private static AvrFuses MagicFuses
        {
            get { return new AvrFuses(0xd8, 0xef, 0xfd); }
        }

        public IEnumerable<AvrBootloaderInformation> GetBootloaderInformations() { return _catalog; }

        private static AvrBootloaderInformation GetDimaAvrBootloaderInformation(string DeviceName, int bootloaderAddress, string BodyResourceName)
        {
            return new AvrBootloaderInformation(DeviceName, MagicFuses,
                                                new ResourceBodyLoader(_assembly, BodyResourceName),
                                                new Placement<AvrMemoryKind>(AvrMemoryKind.Flash, bootloaderAddress),
                                                new Placement<AvrMemoryKind>(AvrMemoryKind.Flash, bootloaderAddress - 0x200),
                                                new Placement<AvrMemoryKind>(AvrMemoryKind.Flash, bootloaderAddress - 0x100));
        }
    }
}
