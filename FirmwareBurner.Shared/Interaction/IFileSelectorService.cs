using System;

namespace FirmwareBurner.Interaction
{
    /// <summary>Сервис взаимодействия с пользователем по поводу выбора файлов</summary>
    public interface IFileSelectorService
    {
        /// <summary>Спрашивает пользователя о месте сохранения файла</summary>
        /// <param name="Arguments">Аргументы запроса</param>
        /// <returns>Путь к месту расположения файла</returns>
        String RequestSaveFileLocation(SaveFileRequestArguments Arguments);
    }
}
