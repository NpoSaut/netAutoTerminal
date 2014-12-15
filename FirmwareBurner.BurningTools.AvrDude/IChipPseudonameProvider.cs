namespace FirmwareBurner.BurningTools.AvrDude
{
    /// <summary>Словарь псевдонимов названий процессоров</summary>
    public interface IChipPseudonameProvider
    {
        /// <summary>Получает псевдоним названия процессора для указанного устройства</summary>
        /// <param name="DeviceName">Имя устройства</param>
        string GetChipPseudoname(string DeviceName);
    }
}
