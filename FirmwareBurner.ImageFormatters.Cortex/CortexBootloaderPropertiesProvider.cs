using System.Collections.Generic;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    internal class CortexBootloaderPropertiesProvider : IPropertiesProvider
    {
        private readonly TargetInformation _target;
        public CortexBootloaderPropertiesProvider(TargetInformation Target) { _target = Target; }

        public IEnumerable<ParamRecord> GetProperties() { yield return new ParamRecord(198, КАК_ТО_ОПРЕДЕЛИТЬ_КОНФИГУРАЦИЮ_ЗАГРУЗЧИКА); }
    }
}
