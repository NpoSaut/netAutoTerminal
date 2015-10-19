using System.Collections.Generic;
using FirmwareBurner.ImageFormatters.Cortex.Exceptions;

namespace FirmwareBurner.ImageFormatters.Cortex.Catalog
{
    internal class StaticBootloaderConfigurationCatalog : IBootloaderConfigurationCatalog
    {
        private static readonly IDictionary<int, int> _configurations =
            new Dictionary<int, int>();

        public int GetConfiguration(TargetInformation Target)
        {
            int configuration;
            if (!_configurations.TryGetValue(Target.CellId, out configuration))
                throw new BootloaderConfigurationNotFoundException();
            return configuration;
        }
    }
}
