namespace FirmwareBurner.BurningTools.AvrDude
{
    /// <summary>Словарь псевдонимов названий процессоров</summary>
    public interface IAvrDudeChipPseudonameProvider
    {
        /// <summary>Получает псевдоним названия процессора для указанного устройства</summary>
        /// <param name="DeviceName">Имя устройства</param>
        string GetChipPseudoname(string DeviceName);
    }
}
