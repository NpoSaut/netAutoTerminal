namespace FirmwarePacker.Project.Serializers
{
    public interface IProjectSerializer
    {
        PackageProject Load(string FileName);
        void Save(PackageProject Project, string FileName);
        string FileExtension { get; }
    }
}