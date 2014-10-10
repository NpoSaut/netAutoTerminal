using FirmwarePacking;

namespace FirmwareBurner.Models.Project
{
    /// <summary>Проект модуля</summary>
    public class ModuleProject
    {
        /// <summary>Информация о модуле</summary>
        public ModuleInformation Information { get; private set; }

        /// <summary>Информация о прошивке</summary>
        public PackageInformation FirmwareInformation { get; private set; }

        /// <summary>Содержимое прошивки</summary>
        public FirmwareComponent FirmwareContent { get; private set; }
    }
}
