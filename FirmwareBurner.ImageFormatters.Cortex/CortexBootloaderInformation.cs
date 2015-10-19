using FirmwareBurner.ImageFormatters.Binary;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    public class CortexBootloaderInformation : BinaryBootloaderInformation<CortexMemoryKind>
    {
        private readonly byte[] _bootloaderBody;

        public CortexBootloaderInformation(Placement<CortexMemoryKind> BootloaderPlacement, Placement<CortexMemoryKind> FilesTablePlacement,
                                           byte[] BootloaderBody) : base(BootloaderPlacement, FilesTablePlacement) { _bootloaderBody = BootloaderBody; }

        public Placement<CortexMemoryKind> StaticPropertiesPlacement { get; private set; }
        public Placement<CortexMemoryKind> DynamicPropertiesPlacement { get; private set; }

        public override byte[] GetBootloaderBody() { return _bootloaderBody; }
    }
}
