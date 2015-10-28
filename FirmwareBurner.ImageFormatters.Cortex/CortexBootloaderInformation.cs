using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    public class CortexBootloaderInformation : BinaryBootloaderInformation<CortexMemoryKind>
    {
        public CortexBootloaderInformation(Placement<CortexMemoryKind> StaticPropertiesPlacement, Placement<CortexMemoryKind> DynamicPropertiesPlacement,
                                           Placement<CortexMemoryKind> BootloaderPlacement, IBodyLoader BodyLoader, BootloaderApi Api)
            : base(BootloaderPlacement, BodyLoader)
        {
            this.Api = Api;
            this.DynamicPropertiesPlacement = DynamicPropertiesPlacement;
            this.StaticPropertiesPlacement = StaticPropertiesPlacement;
        }

        public Placement<CortexMemoryKind> StaticPropertiesPlacement { get; private set; }
        public Placement<CortexMemoryKind> DynamicPropertiesPlacement { get; private set; }
        public BootloaderApi Api { get; private set; }
    }
}
