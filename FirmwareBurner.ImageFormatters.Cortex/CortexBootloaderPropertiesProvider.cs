using System.Collections.Generic;
using FirmwareBurner.ImageFormatters.Cortex.Catalog;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    internal class CortexBootloaderPropertiesProvider : IPropertiesProvider
    {
        private readonly IBootloaderConfigurationCatalog _configurationCatalog;
        private readonly TargetInformation _target;

        public CortexBootloaderPropertiesProvider(TargetInformation Target, IBootloaderConfigurationCatalog ConfigurationCatalog)
        {
            _target = Target;
            _configurationCatalog = ConfigurationCatalog;
        }

        public IEnumerable<ParamRecord> GetProperties() { yield return new ParamRecord(198, _configurationCatalog.GetConfiguration(_target)); }
    }
}
