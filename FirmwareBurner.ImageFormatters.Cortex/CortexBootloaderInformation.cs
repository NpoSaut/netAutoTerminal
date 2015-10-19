using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    public class CortexBootloaderInformation : BinaryBootloaderInformation<CortexMemoryKind>
    {
        public CortexBootloaderInformation(Placement<CortexMemoryKind> StaticPropertiesPlacement, Placement<CortexMemoryKind> DynamicPropertiesPlacement,
                                           Placement<CortexMemoryKind> BootloaderPlacement, IBodyLoader BodyLoader)
            : base(BootloaderPlacement, BodyLoader)
        {
            this.DynamicPropertiesPlacement = DynamicPropertiesPlacement;
            this.StaticPropertiesPlacement = StaticPropertiesPlacement;
        }

        public Placement<CortexMemoryKind> StaticPropertiesPlacement { get; private set; }
        public Placement<CortexMemoryKind> DynamicPropertiesPlacement { get; private set; }
    }
}
