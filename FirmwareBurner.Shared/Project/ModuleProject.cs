using FirmwarePacking;

namespace FirmwareBurner.Models.Project
{
    /// <summary>Проект модуля</summary>
    public class ModuleProject
    {
        public ModuleProject(ModuleInformation Information, PackageInformation FirmwareInformation, FirmwareComponent FirmwareContent)
        {
            this.Information = Information;
            this.FirmwareInformation = FirmwareInformation;
            this.FirmwareContent = FirmwareContent;
        }

        /// <summary>Информация о модуле</summary>
        public ModuleInformation Information { get; private set; }

        /// <summary>Информация о прошивке</summary>
        public PackageInformation FirmwareInformation { get; private set; }

        /// <summary>Содержимое прошивки</summary>
        public FirmwareComponent FirmwareContent { get; private set; }
    }
}
