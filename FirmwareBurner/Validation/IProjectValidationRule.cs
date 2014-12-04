using System;
using System.Collections.Generic;
using FirmwareBurner.Project;

namespace FirmwareBurner.Validation
{
    /// <summary>Правило проверки проекта</summary>
    public interface IProjectValidationRule
    {
        /// <summary>Проверяет указанный проект на валидность</summary>
        /// <param name="Project">Проверяемый проект</param>
        /// <returns>Список противоречий правилу в проекте</returns>
        IEnumerable<String> ValidateProject(FirmwareProject Project);
    }
}
