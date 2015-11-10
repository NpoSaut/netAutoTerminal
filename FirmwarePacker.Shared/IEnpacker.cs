using FirmwarePacker.Project;
using FirmwarePacking;

namespace FirmwarePacker.Shared
{
    public interface IEnpacker
    {
        FirmwarePackage Enpack(PackageProject Project, PackageVersion Version);
    }
}
