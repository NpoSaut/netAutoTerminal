using FirmwareBurner.ViewModels.Property;

namespace FirmwareBurner.Validation
{
    /// <summary>Контекст валидации</summary>
    public interface IValidationContext
    {
        /// <summary>Регистрирует валидируемый объект в контексте валидации</summary>
        /// <param name="Validateable">Регистрируемый валидируемый объект</param>
        void RegisterValidateableElement(IValidateable Validateable);

        /// <summary>Проверяет все зарегистрированные валидаторы</summary>
        /// <returns>True, если контекст не содержит ошибок</returns>
        bool Check();
    }
}
