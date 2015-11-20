using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    public class CortexBootloaderInformation : BinaryBootloaderInformation<CortexMemoryKind>
    {
        public CortexBootloaderInformation(string DeviceName, BootloaderApi Api, Placement<CortexMemoryKind> StaticPropertiesPlacement,
                                           Placement<CortexMemoryKind> DynamicPropertiesPlacement, Placement<CortexMemoryKind> BootloaderPlacement, IBodyLoader BodyLoader)
            : base(DeviceName, BootloaderPlacement, BodyLoader, Api)
        {
            this.DynamicPropertiesPlacement = DynamicPropertiesPlacement;
            this.StaticPropertiesPlacement = StaticPropertiesPlacement;
        }

        public Placement<CortexMemoryKind> StaticPropertiesPlacement { get; private set; }
        public Placement<CortexMemoryKind> DynamicPropertiesPlacement { get; private set; }
    }
}
