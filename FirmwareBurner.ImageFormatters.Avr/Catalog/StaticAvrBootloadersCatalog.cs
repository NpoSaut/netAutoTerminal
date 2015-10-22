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
        private readonly IDictionary<string, AvrBootloaderInformation> _catalog;

        public StaticAvrBootloadersCatalog()
        {
            _catalog =
                new Dictionary<string, AvrBootloaderInformation>
                {
                    { "at90can128", GetDimaAvrBootloaderInformation(0x1e000, "Bootloaders.Bootloader_128k") },
                    { "at90can64", GetDimaAvrBootloaderInformation(0x0e000, "Bootloaders.Bootloader_64k") }
                };
        }

        /// <summary>Волшебные FUSE-биты</summary>
        private static AvrFuses MagicFuses
        {
            get { return new AvrFuses(0xd8, 0xef, 0xfd); }
        }

        /// <summary>Находит информацию о загрузчике для указанного устройства</summary>
        /// <param name="DeviceName">Название типа устройства</param>
        public AvrBootloaderInformation GetBootloaderInformation(string DeviceName)
        {
            return _catalog[DeviceName];
        }

        private static AvrBootloaderInformation GetDimaAvrBootloaderInformation(int bootloaderAddress, string BodyResourceName)
        {
            return new AvrBootloaderInformation(MagicFuses,
                                                new ResourceBodyLoader(_assembly, BodyResourceName),
                                                new Placement<AvrMemoryKind>(AvrMemoryKind.Flash, bootloaderAddress),
                                                new Placement<AvrMemoryKind>(AvrMemoryKind.Flash, bootloaderAddress - 0x200),
                                                new Placement<AvrMemoryKind>(AvrMemoryKind.Flash, bootloaderAddress - 0x100));
        }
    }
}
