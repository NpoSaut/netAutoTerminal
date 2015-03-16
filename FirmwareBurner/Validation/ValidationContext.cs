using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.ViewModels.Property;

namespace FirmwareBurner.Validation
{
    public class ValidationContext : IValidationContext
    {
        private readonly IList<IValidateable> _validateableElements;
        public ValidationContext() { _validateableElements = new List<IValidateable>(); }

        /// <summary>Регистрирует валидируемый объект в контексте валидации</summary>
        /// <param name="Validateable">Регистрируемый валидируемый объект</param>
        public void RegisterValidateableElement(IValidateable Validateable) { _validateableElements.Add(Validateable); }

        /// <summary>Проверяет все зарегистрированные валидаторы</summary>
        /// <returns>True, если контекст не содержит ошибок</returns>
        public bool Check() { return _validateableElements.All(i => i.IsValid); }
    }
}
