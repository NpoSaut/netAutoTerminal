using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Project;
using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.Validation.Rules
{
    /// <summary>Проверяет наличие прошивки в проекте</summary>
    public class HaveFirmwareValidationRule : IProjectValidationRule
    {
        private readonly IIndexHelper _index;
        public HaveFirmwareValidationRule(IIndexHelper Index) { _index = Index; }

        /// <summary>Проверяет указанный проект на валидность</summary>
        /// <param name="Project">Проверяемый проект</param>
        /// <returns>Список противоречий правилу в проекте</returns>
        public IEnumerable<string> ValidateProject(FirmwareProject Project)
        {
            return Project.Modules
                          .Where(m => m.FirmwareInformation == null || m.FirmwareContent == null)
                          .Select(m => String.Format("Отсутствует прошивка для модуля {0}", GetModuleName(Project.Target.CellId, m.Information.ModuleId)));
        }

        private string GetModuleName(int CellId, int ModuleId) { return _index.GetModuleName(CellId, ModuleId); }
    }
}
