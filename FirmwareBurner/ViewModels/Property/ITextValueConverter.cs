using System.Collections.Generic;

namespace FirmwareBurner.ViewModels.Property
{
    /// <summary>Конвертер из строки в объект</summary>
    /// <typeparam name="TValue">Тип объекта для конверсии</typeparam>
    public interface ITextValueConverter<TValue>
    {
        /// <summary>Конвертирует строку в объект</summary>
        /// <param name="Text">Строковое значение</param>
        /// <param name="Value">Выходное значение</param>
        /// <returns>Список ошибок конвертации</returns>
        IEnumerable<string> TryConvert(string Text, out TValue Value);

        /// <summary>Получает строковое представление объекта</summary>
        /// <param name="Value">Значение объекта</param>
        /// <returns>Строковое представление</returns>
        string GetText(TValue Value);
    }
}
