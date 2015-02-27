using System.Collections.Generic;
using System.Reflection;

namespace FirmwareBurner.ImageFormatters.Avr
{
    /// <summary>Захардкоженый каталог загрузчиков</summary>
    public class StaticAvrBootloadersCatalog : IAvrBootloadersCatalog
    {
        private readonly IDictionary<string, AvrBootloaderInformation> _catalog;

        public StaticAvrBootloadersCatalog()
        {
            var assembly = Assembly.GetAssembly(typeof (StaticAvrBootloadersCatalog));
            _catalog =
                new Dictionary<string, AvrBootloaderInformation>
                {
                    { "at90can128", GetDimaAvrBootloaderInformation(0x1e000, assembly.GetName().Name + ".Bootloaders.bootloader") },
                    { "at90can64", GetDimaAvrBootloaderInformation(0x0e000, assembly.GetName().Name + ".Bootloaders.bootloader") }
                };
        }

        /// <summary>Волшебные FUSE-биты</summary>
        private static AvrFuses MagicFuses
        {
            get { return new AvrFuses(0xd8, 0xef, 0xfd); }
        }

        /// <summary>Находит информацию о загрузчике для указанного устройства</summary>
        /// <param name="DeviceName">Название типа устройства</param>
        public AvrBootloaderInformation GetBootloaderInformation(string DeviceName) { return _catalog[DeviceName]; }

        private static AvrBootloaderInformation GetDimaAvrBootloaderInformation(int bootloaderAddress, string BodyResourceName)
        {
            var placements = new PlacementsInformation(bootloaderAddress, bootloaderAddress - 0x200, bootloaderAddress - 0x100);
            return new AvrBootloaderInformation(MagicFuses, placements, BodyResourceName);
        }
    }
}
