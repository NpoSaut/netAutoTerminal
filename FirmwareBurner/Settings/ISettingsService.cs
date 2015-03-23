using System;
using FirmwareBurner.Annotations;

namespace FirmwareBurner.Settings
{
    /// <summary>Сервис настроек приложения</summary>
    public interface ISettingsService
    {
        /// <summary>Находит имя предопочитаемого метода прошивки для указанного типа ячейки</summary>
        /// <param name="CellId">Тип ячейки</param>
        /// <returns>Имя предпочитаемого метода прошивки, или null, если метод по-умолчанию не задан</returns>
        [CanBeNull]
        String GetPreferredBurningMethod(int CellId);

        /// <summary>Задаёт предопочитаемого метода прошивки для указанного типа ячейки</summary>
        /// <param name="CellId">Тип ячейки</param>
        /// <param name="MethodName">Имя предпочитаемого метода прошивки</param>
        void SetPreferredBurningMethod(int CellId, String MethodName);

        /// <summary>Сохраняет настройки приложения</summary>
        void Save();
    }
}
