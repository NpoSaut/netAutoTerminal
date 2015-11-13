using FirmwarePacker.Project;
using FirmwarePacking;

namespace FirmwarePacker.Shared
{
    public class PackageSavingTool : IPackageSavingTool
    {
        private readonly IEnpacker _enpacker;
        public PackageSavingTool(IEnpacker Enpacker) { _enpacker = Enpacker; }

        public void SavePackage(PackageProject Model, PackageVersion Version, string FileName, string RootDirectory)
        {
            _enpacker.Enpack(Model, Version, RootDirectory).Save(FileName);
        }

        public string FileExtension
        {
            get { return FirmwarePackage.FirmwarePackageExtension; }
        }
    }
}
