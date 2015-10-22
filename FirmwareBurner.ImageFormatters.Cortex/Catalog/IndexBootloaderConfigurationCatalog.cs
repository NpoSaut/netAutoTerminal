using System;
using System.Globalization;
using FirmwareBurner.ImageFormatters.Cortex.Exceptions;
using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.ImageFormatters.Cortex.Catalog
{
    public class IndexBootloaderConfigurationCatalog : IBootloaderConfigurationCatalog
    {
        private readonly IIndexHelper _indexHelper;
        public IndexBootloaderConfigurationCatalog(IIndexHelper IndexHelper) { _indexHelper = IndexHelper; }

        public int GetConfiguration(TargetInformation Target)
        {
            ModificationKind modification = _indexHelper.GetModification(Target.CellId, Target.ModificationId);
            string configurationString = modification.CustomProperties["config"];

            int configuration;
            if (configurationString == null ||
                !Int32.TryParse(configurationString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out configuration))
                throw new BootloaderConfigurationNotFoundException();

            return configuration;
        }
    }
}
