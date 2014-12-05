using System;
using System.Collections.Generic;
using FirmwareBurner.Project;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner.Validation
{
    /// <summary>Валидатор проектов</summary>
    public interface IProjectValidator
    {
        /// <summary>Проверяет проект и возвращает список ошибок</summary>
        /// <param name="Project">Проверяемый проект</param>
        /// <returns>Тексты ошибок валидации</returns>
        IEnumerable<String> GetValidationErrors(ProjectViewModel Project);
    }
}
