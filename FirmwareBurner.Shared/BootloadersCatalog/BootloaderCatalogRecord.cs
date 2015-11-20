using System.Collections.Generic;

namespace FirmwareBurner.BootloadersCatalog
{
    public class BootloaderCatalogRecord
    {
        public BootloaderCatalogRecord(int Id, string TargetDevice, int Version, int CompatibleVersion, string FileName, IDictionary<string, string> Properties)
        {
            this.Properties = Properties;
            this.Id = Id;
            this.TargetDevice = TargetDevice;
            this.Version = Version;
            this.CompatibleVersion = CompatibleVersion;
            this.FileName = FileName;
        }

        public int Id { get; private set; }
        public string TargetDevice { get; private set; }
        public int Version { get; private set; }
        public int CompatibleVersion { get; private set; }
        public string FileName { get; private set; }
        public IDictionary<string, string> Properties { get; private set; }
    }
}
