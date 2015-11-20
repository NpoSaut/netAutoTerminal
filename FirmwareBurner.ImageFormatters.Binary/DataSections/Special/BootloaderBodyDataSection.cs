using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Binary.DataSections.Special
{
    public class BootloaderBodyDataSection<TMemoryKind> : IDataSection<TMemoryKind>
    {
        private readonly BinaryBootloaderInformation<TMemoryKind> _bootloaderInformation;
        public BootloaderBodyDataSection(BinaryBootloaderInformation<TMemoryKind> BootloaderInformation) { _bootloaderInformation = BootloaderInformation; }

        public Placement<TMemoryKind> Placement
        {
            get { return _bootloaderInformation.BootloaderPlacement; }
        }

        public void WriteTo(IBuffer Buffer) { Buffer.Write(Placement.Address, _bootloaderInformation.GetBootloaderBody()); }
    }
}
