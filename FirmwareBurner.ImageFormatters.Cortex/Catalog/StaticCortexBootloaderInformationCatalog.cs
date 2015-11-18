using System.Collections.Generic;
using System.Reflection;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Cortex.Catalog
{
    public class StaticCortexBootloaderInformationCatalog : ICortexBootloaderInformationCatalog
    {
        private static readonly ICollection<CortexBootloaderInformation> _list =
            new[]
            {
                CreateInformationInstance("stm32f4", "stm32f4", 0x08000000, 0x08003E00, 0x08004000, 3, 1, 2),
                CreateInformationInstance("at91sam7a3", "at91sam7a3", 0x00100000, 0x00102E00, 0x00103000, 4, 1, 2),
                CreateInformationInstance("mdr32f9q2i", "mdr32f9q2i", 0x08000000, 0x08002E00, 0x08003000, 5, 1, 2)
            };

        public IEnumerable<CortexBootloaderInformation> GetBootloaderInformations() { return _list; }

        private static CortexBootloaderInformation CreateInformationInstance(string DeviceName, string BootloaderName, int BootloaderAddress,
                                                                             int StaticPropertiesAddress,
                                                                             int DynamicPropertiesAddress, int ApiId, int CompatibleVersion, int CurrentVersion)
        {
            return new CortexBootloaderInformation(DeviceName, new Placement<CortexMemoryKind>(CortexMemoryKind.Flash, StaticPropertiesAddress),
                                                   new Placement<CortexMemoryKind>(CortexMemoryKind.Flash, DynamicPropertiesAddress),
                                                   new Placement<CortexMemoryKind>(CortexMemoryKind.Flash, BootloaderAddress),
                                                   new ResourceBodyLoader(Assembly.GetAssembly(typeof (StaticCortexBootloaderInformationCatalog)),
                                                                          string.Format("Bootloaders.{0}.bin", BootloaderName)),
                                                   new BootloaderApi(ApiId, CurrentVersion, CompatibleVersion));
        }
    }
}
