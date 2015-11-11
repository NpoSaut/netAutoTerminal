using FirmwarePacker.Project;

namespace FirmwarePacker.Shared
{
    public class PackageSavingTool : IPackageSavingTool
    {
        private readonly IEnpacker _enpacker;
        public PackageSavingTool(IEnpacker Enpacker) { _enpacker = Enpacker; }

        public void SavePackage(PackageProject Model, PackageVersion Version, string FileName) { _enpacker.Enpack(Model, Version).Save(FileName); }
    }
}