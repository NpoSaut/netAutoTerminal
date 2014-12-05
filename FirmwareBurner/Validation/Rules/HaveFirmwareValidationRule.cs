using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner.Validation.Rules
{
    /// <summary>Проверяет наличие прошивки в проекте</summary>
    public class HaveFirmwareValidationRule : IProjectValidationRule
    {
        /// <summary>Проверяет указанный проект на валидность</summary>
        /// <param name="Project">Проверяемый проект</param>
        /// <returns>Список противоречий правилу в проекте</returns>
        public IEnumerable<string> ValidateProject(ProjectViewModel Project)
        {
            return Project.FirmwareSetConstructor.Components
                          .Where(c => !c.FirmwareSelector.IsPackageSelected)
                          .Select(m => String.Format("Не выбрана для прошивка модуля \"{0}\"", m.ModuleName));
        }
    }
}
