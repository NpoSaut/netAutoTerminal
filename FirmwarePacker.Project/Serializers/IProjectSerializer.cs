namespace FirmwarePacker.Project.Serializers
{
    public interface IProjectSerializer
    {
        PackageProject Load();
        void Save(PackageProject Project);
    }
}