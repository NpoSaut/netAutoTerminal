namespace FirmwarePacker.Project
{
    public class BootloaderRequirement
    {
        public BootloaderRequirement(int BootloaderId, int Version, int CompatibleVersion)
        {
            this.BootloaderId = BootloaderId;
            this.Version = Version;
            this.CompatibleVersion = CompatibleVersion;
        }

        public int BootloaderId { get; private set; }
        public int Version { get; private set; }
        public int CompatibleVersion { get; private set; }
    }
}
