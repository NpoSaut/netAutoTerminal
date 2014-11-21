using System.Collections.Generic;

namespace FirmwareBurner.Models.Project
{
    /// <summary>Проект прошивки</summary>
    public class FirmwareProject
    {
        public FirmwareProject(TargetInformation Target, IList<ModuleProject> Modules)
        {
            this.Modules = Modules;
            this.Target = Target;
        }

        /// <summary>Информация о цели прошивки</summary>
        public TargetInformation Target { get; private set; }

        /// <summary>Список модулей в прошивке</summary>
        public IList<ModuleProject> Modules { get; private set; }
    }
}
