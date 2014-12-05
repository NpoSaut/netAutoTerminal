using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Project;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner.Validation.Rules
{
    /// <summary>Проверяет правильность серийного номера</summary>
    public class SerialNumberValidationRule : IProjectValidationRule
    {
        /// <summary>Проверяет указанный проект на валидность</summary>
        /// <param name="Project">Проверяемый проект</param>
        /// <returns>Список противоречий правилу в проекте</returns>
        public IEnumerable<string> ValidateProject(ProjectViewModel Project)
        {
            if (Project.BlockDetails.SerialNumber > 0) return Enumerable.Empty<String>();
            return new [] { "Не введён серийный номер ячейки" };
        }
    }
}
