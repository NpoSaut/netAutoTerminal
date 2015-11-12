using FirmwarePacker.Project;

namespace FirmwarePacker.Shared
{
    public interface IVariablesProcessor
    {
        string ReplaceVariables(string Source, PackageProject Project, PackageVersion Version);
    }
}
