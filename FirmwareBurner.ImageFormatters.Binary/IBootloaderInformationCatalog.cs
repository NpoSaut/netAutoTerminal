namespace FirmwareBurner.ImageFormatters.Binary
{
    public interface IBootloaderInformationCatalog<out TBootloaderInformation, TMemoryKind>
        where TBootloaderInformation : BinaryBootloaderInformation<TMemoryKind>
    {
        TBootloaderInformation GetBootloaderInformation(string DeviceName);
    }
}
