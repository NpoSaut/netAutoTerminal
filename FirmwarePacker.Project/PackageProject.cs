using System.Collections.Generic;

namespace FirmwarePacker.Project
{
    /// <summary>Проект пакета</summary>
    public class PackageProject
    {
        public PackageProject(PackageVersion Version, ICollection<ComponentProject> Components)
        {
            this.Version = Version;
            this.Components = Components;
        }

        public PackageVersion Version { get; private set; }
        public ICollection<ComponentProject> Components { get; private set; }
    }
}
