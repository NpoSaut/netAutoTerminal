using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;
using FirmwareBurner.ImageFormatters.Cortex;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.UskUsb
{
    public class UskUsbBootloaderInformation : BinaryBootloaderInformation<CortexMemoryKind>
    {
        public UskUsbBootloaderInformation(string DeviceName, Placement<CortexMemoryKind> BootloaderPlacement, Placement<CortexMemoryKind> FileTablePlacement,
                                           Placement<CortexMemoryKind> PropertiesPlacement, IBodyLoader BodyLoader, BootloaderApi Api)
            : base(DeviceName, BootloaderPlacement, BodyLoader, Api)
        {
            this.FileTablePlacement = FileTablePlacement;
            this.PropertiesPlacement = PropertiesPlacement;
        }

        public Placement<CortexMemoryKind> FileTablePlacement { get; private set; }
        public Placement<CortexMemoryKind> PropertiesPlacement { get; private set; }
    }
}
