namespace FirmwareBurner.ImageFormatters.Binary
{
    public abstract class BinaryBootloaderInformation<TMemoryKind>
    {
        protected BinaryBootloaderInformation(Placement<TMemoryKind> BootloaderPlacement,
                                           Placement<TMemoryKind> FilesTablePlacement)
        {
            this.BootloaderPlacement = BootloaderPlacement;
            this.FilesTablePlacement = FilesTablePlacement;
        }

        public Placement<TMemoryKind> BootloaderPlacement { get; private set; }
        public Placement<TMemoryKind> FilesTablePlacement { get; private set; }
        public abstract byte[] GetBootloaderBody();
    }
}
