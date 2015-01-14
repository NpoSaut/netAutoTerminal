using System;
using System.Collections.Generic;

namespace FirmwareBurner.Validation.Rules
{
    /// <summary>Проверяет правильность даты производства ячейки</summary>
    public class AssemblyDateValidationRule : IValidationRule<DateTime>
    {
        private static readonly DateTime _minimalDateTime = new DateTime(1991, 1, 1);

        /// <summary>Проверяет значение свойства и возвращает список ошибок</summary>
        /// <param name="PropertyValue">Значение проверяемого свойство</param>
        /// <returns>Тексты ошибок валидации</returns>
        public IEnumerable<string> GetValidationErrors(DateTime PropertyValue)
        {
            if (PropertyValue < _minimalDateTime)
                yield return String.Format("Дата сборки слишком мала");
            if (PropertyValue > DateTime.Now)
                yield return String.Format("Дата сборки не может быть в будущем");
        }
    }
}
