using System.Collections.Generic;

namespace FirmwarePacker.Project
{
    /// <summary>Проект пакета</summary>
    public class PackageProject
    {
        public PackageProject(ICollection<ComponentProject> Components) { this.Components = Components; }

        public ICollection<ComponentProject> Components { get; private set; }
    }
}
