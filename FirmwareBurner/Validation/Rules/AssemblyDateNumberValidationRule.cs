using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Project;

namespace FirmwareBurner.Validation.Rules
{
    /// <summary>Проверяет правильность даты производства ячейки</summary>
    public class AssemblyDateValidationRule : IProjectValidationRule
    {
        private static readonly DateTime _minimalDateTime = new DateTime(1991, 1, 1);

        /// <summary>Проверяет указанный проект на валидность</summary>
        /// <param name="Project">Проверяемый проект</param>
        /// <returns>Список противоречий правилу в проекте</returns>
        public IEnumerable<string> ValidateProject(FirmwareProject Project)
        {
            if (Project.Target.AssemblyDate > _minimalDateTime) return Enumerable.Empty<String>();
            return new[] { "Не указана дата сборки ячейки" };
        }
    }
}
