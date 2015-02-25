using System.Collections.Generic;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner.Validation.Rules
{
    /// <summary>Проверяет наличие прошивки в проекте</summary>
    public class HaveFirmwareValidationRule : IValidationRule<FirmwareSetComponentViewModel>
    {
        /// <summary>Проверяет значение свойства и возвращает список ошибок</summary>
        /// <param name="FirmwareSetComponent">Значение проверяемого свойство</param>
        /// <returns>Тексты ошибок валидации</returns>
        public IEnumerable<string> GetValidationErrors(FirmwareSetComponentViewModel FirmwareSetComponent)
        {
            if (!FirmwareSetComponent.FirmwareSelector.IsPackageSelected) yield return "Прошивка не выбрана";
        }
    }
}
