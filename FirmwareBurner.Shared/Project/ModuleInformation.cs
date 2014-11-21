namespace FirmwareBurner.Project
{
    /// <summary>Информация о модуле</summary>
    public class ModuleInformation
    {
        public ModuleInformation(int ModuleId) { this.ModuleId = ModuleId; }

        /// <summary>Идентификатор модуля</summary>
        public int ModuleId { get; private set; }
    }
}
