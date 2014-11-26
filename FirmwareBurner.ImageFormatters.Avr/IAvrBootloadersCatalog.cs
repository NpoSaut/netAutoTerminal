using System;

namespace FirmwareBurner.ImageFormatters.Avr
{
    /// <summary>Каталог загрузчиков</summary>
    public interface IAvrBootloadersCatalog
    {
        /// <summary>Находит информацию о загрузчике для указанного устройства</summary>
        /// <param name="DeviceName">Название типа устройства</param>
        AvrBootloaderInformation GetBootloaderInformation(String DeviceName);
    }
}
