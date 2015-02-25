using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.ViewModels;
using FirmwareBurner.ViewModels.Property;

namespace FirmwareBurner.Validation
{
    /// <summary>��������� ������, ��������� �������������� ������ ��������� ���� <see cref="IValidateable" />
    /// </summary>
    public class ValidationPropertyCheckProjectValidator : IProjectValidator
    {
        /// <summary>��������� ������ � ���������� ������ ������</summary>
        /// <param name="Project">����������� ������</param>
        /// <returns>������ ������ ���������</returns>
        public IEnumerable<string> GetValidationErrors(ProjectViewModel Project)
        {
            IEnumerable<IValidateable> validators = GetValidators(Project);
            return validators.SelectMany(v => v.ValidationErrors);
        }

        /// <summary>�������� ������ ���� ����������� ��� ������ ������������� �������</summary>
        /// <param name="Project">������ ������������� �������</param>
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