using FirmwarePacker.Project;

namespace FirmwarePacker.Enpacking
{
    public interface IPackageSavingTool
    {
        void SavePackage(PackageProject Model, PackageVersion Version, string FileName, string RootDirectory);
        string FileExtension { get; }
    }
}
