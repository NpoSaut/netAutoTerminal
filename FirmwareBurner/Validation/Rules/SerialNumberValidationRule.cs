using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Project;

namespace FirmwareBurner.Validation.Rules
{
    /// <summary>Проверяет правильность серийного номера</summary>
    public class SerialNumberValidationRule : IProjectValidationRule
    {
        /// <summary>Проверяет указанный проект на валидность</summary>
        /// <param name="Project">Проверяемый проект</param>
        /// <returns>Список противоречий правилу в проекте</returns>
        public IEnumerable<String> ValidateProject(FirmwareProject Project)
        {
            if (Project.Target.SerialNumber > 0) return Enumerable.Empty<String>();
            return new [] { "Не введён серийный номер ячейки" };
        }
    }
}
