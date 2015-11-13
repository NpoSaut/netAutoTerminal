using System.Collections.Generic;

namespace FirmwarePacker.Project
{
    /// <summary>Проект компонента прошивки</summary>
    public class ComponentProject
    {
        public ComponentProject(ICollection<ComponentProjectTarget> Targets, ICollection<IFileMap> FileMaps)
        {
            this.Targets = Targets;
            this.FileMaps = FileMaps;
        }

        /// <summary>Цели компонента</summary>
        public ICollection<ComponentProjectTarget> Targets { get; private set; }

        /// <summary>Маппинги файлов</summary>
        public ICollection<IFileMap> FileMaps { get; private set; }
    }
}
