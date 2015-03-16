using System.Collections.Generic;

namespace FirmwareBurner.Validation.Rules
{
    /// <summary>Проверяет правильность серийного номера</summary>
    public class SerialNumberValidationRule : IValidationRule<int>
    {
        /// <summary>Проверяет значение свойства и возвращает список ошибок</summary>
        /// <param name="PropertyValue">Значение проверяемго свойство</param>
        /// <returns>Тексты ошибок валидации</returns>
        public IEnumerable<string> GetValidationErrors(int PropertyValue)
        {
            if (PropertyValue <= 10000)
                yield return "Введён не верный серийный номер ячейки";
            if (PropertyValue > 99999)
                yield return "Введён слишком большой серийный номер ячейки";
        }
    }
}
