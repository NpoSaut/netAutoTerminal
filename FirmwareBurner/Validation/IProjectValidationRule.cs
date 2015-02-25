using System.Collections.Generic;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner.Validation
{
    /// <summary>Правило проверки проекта</summary>
    public interface IProjectValidationRule
    {
        /// <summary>Проверяет указанный проект на валидность</summary>
        /// <param name="Project">Проверяемый проект</param>
        /// <returns>Список противоречий правилу в проекте</returns>
        IEnumerable<string> ValidateProject(ProjectViewModel Project);
    }
}
