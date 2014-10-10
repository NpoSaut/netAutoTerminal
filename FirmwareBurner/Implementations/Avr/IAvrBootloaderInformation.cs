using System;
using FirmwareBurner.Models.Images.Binary;

namespace FirmwareBurner.Implementations.Avr
{
    /// <summary>Информация о загрузчике для AVR-устройств</summary>
    public interface IAvrBootloaderInformation
    {
        /// <summary>Необходимые значения FUSE-битов</summary>
        AvrFuses RequiredFuses { get; }

        /// <summary>Адрес расположения загрузчика в памяти контроллера</summary>
        Int32 BootloaderPlacementAddress { get; }

        /// <summary>Компоновщик файловой таблицы</summary>
        IBinaryFilesystemFormatter FilesystemFormatter { get; }

        /// <summary>Компоновщик таблицы свойств</summary>
        IBinaryPropertiesTableFormatter PropertiesTableFormatter { get; }

        /// <summary>Получает тело загрузчика</summary>
        Byte[] GetBootloaderBody();
    }
}
