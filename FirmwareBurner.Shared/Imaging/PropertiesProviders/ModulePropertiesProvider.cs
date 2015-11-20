using System.Collections.Generic;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Project;

namespace FirmwareBurner.Imaging.PropertiesProviders
{
    public class ModulePropertiesProvider : IPropertiesProvider
    {
        private readonly ModuleInformation _moduleInformation;
        public ModulePropertiesProvider(ModuleInformation ModuleInformation) { _moduleInformation = ModuleInformation; }

        public IEnumerable<ParamRecord> GetProperties() { yield return new ParamRecord(130, _moduleInformation.ModuleId); }
    }
}
