using FirmwarePacker.Project;

namespace FirmwarePacker.Shared
{
    public interface IPackageSavingTool
    {
        void SavePackage(PackageProject Model, PackageVersion Version, string FileName, string RootDirectory);
        string FileExtension { get; }
    }
}
