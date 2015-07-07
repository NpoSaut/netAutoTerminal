using System;
using System.Collections.Generic;
using FirmwareBurner.Validation;

namespace FirmwareBurner.ViewModels.Targeting
{
    public class BoundsValidator<T> : IValidationRule<T> where T: IComparable
    {
        private readonly T _maxValue;
        private readonly T _minValue;

        public BoundsValidator(T MinValue, T MaxValue)
        {
            _minValue = MinValue;
            _maxValue = MaxValue;
        }

        /// <summary>Проверяет значение свойства и возвращает список ошибок</summary>
        /// <param name="PropertyValue">Значение проверяемого свойство</param>
        /// <returns>Тексты ошибок валидации</returns>
        public IEnumerable<string> GetValidationErrors(T PropertyValue)
        {
            if (PropertyValue.CompareTo(_minValue) < 0)
                yield return string.Format("Значение должно быть не меньше {0}", _minValue);
            if (PropertyValue.CompareTo(_minValue) < 0)
                yield return string.Format("Значение должно быть не больше {0}", _maxValue);
        }
    }
}
