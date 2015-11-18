using FirmwarePacker.Project;
using FirmwarePacking;

namespace FirmwarePacker.Enpacking
{
    public interface IEnpacker
    {
        FirmwarePackage Enpack(PackageProject Project, PackageVersion Version, string RootDirectory);
    }
}
