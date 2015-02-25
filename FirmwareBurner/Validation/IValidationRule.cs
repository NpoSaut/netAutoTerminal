using System;
using System.Collections.Generic;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner.Validation
{
    public interface IValidationRule<in TValue>
    {
        /// <summary>Проверяет значение свойства и возвращает список ошибок</summary>
        /// <param name="PropertyValue">Значение проверяемого свойство</param>
        /// <returns>Тексты ошибок валидации</returns>
        IEnumerable<String> GetValidationErrors(TValue PropertyValue);
    }
}
