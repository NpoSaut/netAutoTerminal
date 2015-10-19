using System.Collections.Generic;
using System.Reflection;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;

namespace FirmwareBurner.ImageFormatters.Cortex.Catalog
{
    public class StaticCortexBootloaderInformationCatalog : ICortexBootloaderInformationCatalog
    {
        private static readonly IDictionary<string, CortexBootloaderInformation> _dictionary =
            new Dictionary<string, CortexBootloaderInformation>
            {
                { "stm32f4", CreateInformationInstance("stm32f4", 0x08000000, 0x08003E00, 0x08004000) },
                { "at91sam7a3", CreateInformationInstance("at91sam7a3", 0x00100000, 0x00102E00, 0x00103000) },
                { "mdr32f9q2i", CreateInformationInstance("mdr32f9q2i", 0x08000000, 0x08002E00, 0x08003000) }
            };

        public CortexBootloaderInformation GetBootloaderInformation(string DeviceName) { return _dictionary[DeviceName]; }

        private static CortexBootloaderInformation CreateInformationInstance(string BootloaderName, int BootloaderAddress, int StaticPropertiesAddress,
                                                                             int DynamicPropertiesAddress)
        {
            return new CortexBootloaderInformation(
                new Placement<CortexMemoryKind>(CortexMemoryKind.Flash, StaticPropertiesAddress),
                new Placement<CortexMemoryKind>(CortexMemoryKind.Flash, DynamicPropertiesAddress),
                new Placement<CortexMemoryKind>(CortexMemoryKind.Flash, BootloaderAddress),
                new ResourceBodyLoader(Assembly.GetAssembly(typeof (StaticCortexBootloaderInformationCatalog)),
                                       string.Format("Bootloaders.{0}.bin", BootloaderName)));
        }
    }
}
