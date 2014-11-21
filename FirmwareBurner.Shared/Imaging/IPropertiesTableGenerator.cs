using System.Collections.Generic;
using FirmwareBurner.Project;
using FirmwarePacking;

namespace FirmwareBurner.Imaging
{
    /// <summary>Инструмент по формированию таблицы свойств в прошивке</summary>
    public interface IPropertiesTableGenerator
    {
        IDictionary<byte, int> Generate(TargetInformation Target, ModuleInformation Information, PackageInformation FirmwareInformation);
    }
}
