using System.Collections.Generic;

namespace FirmwareBurner.Validation.Rules
{
    /// <summary>Проверяет правильность серийного номера</summary>
    public class SerialNumberLengthValidationRule : IValidationRule<string>
    {
        /// <summary>Проверяет значение свойства и возвращает список ошибок</summary>
        /// <param name="PropertyValue">Значение проверяемго свойство</param>
        /// <returns>Тексты ошибок валидации</returns>
        public IEnumerable<string> GetValidationErrors(string PropertyValue)
        {
            if (PropertyValue.Trim().Length != 5)
                yield return "Длина серийного номера должна быть 5 символов";
        }
    }
}
