namespace FirmwareBurner.ImageFormatters.Cortex.Catalog
{
    public interface IBootloaderConfigurationCatalog
    {
        int GetConfiguration(TargetInformation Target);
    }
}
