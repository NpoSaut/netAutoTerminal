using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.ViewModels;
using FirmwareBurner.ViewModels.Property;

namespace FirmwareBurner.Validation
{
    /// <summary>ѕровер€ет проект, использу€ захардкоженный список элементов типа <see cref="IValidateable" />
    /// </summary>
    public class ValidationPropertyCheckProjectValidator : IProjectValidator
    {
        /// <summary>ѕровер€ет проект и возвращает список ошибок</summary>
        /// <param name="Project">ѕровер€емый проект</param>
        /// <returns>“ексты ошибок валидации</returns>
        public IEnumerable<string> GetValidationErrors(ProjectViewModel Project)
        {
            IEnumerable<IValidateable> validators = GetValidators(Project);
            return validators.SelectMany(v => v.ValidationErrors);
        }

        /// <summary>ѕолучает список всех валидаторов дл€ модели представлени€ проекта</summary>
        /// <param name="Project">ћодель представлени€ проекта</param>
        private static IEnumerable<IValidateable> GetValidators(ProjectViewModel Project)
        {
            IEnumerable<IValidateable> valideatableProperties =
                new IValidateable[]
                {
                    Project.BlockDetails.AssemblyDate,
                    Project.BlockDetails.SerialNumber
                }
                    .Concat(Project.FirmwareSetConstructor.Components);
            return valideatableProperties;
        }
    }
}