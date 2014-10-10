using System.Collections.Generic;

namespace FirmwareBurner.Model.Project
{
    /// <summary>Проект прошивки</summary>
    public class FirmwareProject
    {
        /// <summary>Информация о цели прошивки</summary>
        public TargetInformation Target { get; set; }

        /// <summary>Список модулей в прошивке</summary>
        public IList<ModuleProject> Modules { get; private set; }
    }
}
