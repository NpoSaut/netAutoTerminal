using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Project;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner.Validation
{
    /// <summary>Валидатор проекта, действующий на основе списка правил валидации, переданного ему в конструкторе</summary>
    public class RulesListProjectValidator : IProjectValidator
    {
        private readonly IList<IProjectValidationRule> _validationRules;
        public RulesListProjectValidator(IProjectValidationRule[] ValidationRules) { _validationRules = ValidationRules; }

        /// <summary>Проверяет проект и возвращает список ошибок</summary>
        /// <param name="Project">Проверяемый проект</param>
        /// <returns>Тексты ошибок валидации</returns>
        public IEnumerable<string> GetValidationErrors(ProjectViewModel Project) { return _validationRules.SelectMany(rule => rule.ValidateProject(Project)); }
    }
}
