using FirmwarePacker.Project;

namespace FirmwarePacker
{
    public interface IVariablesProcessor
    {
        string ReplaceVariables(string Source, PackageProject Project, PackageVersion Version);
    }
}
