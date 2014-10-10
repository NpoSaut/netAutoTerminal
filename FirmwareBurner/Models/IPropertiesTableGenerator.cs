using System.Collections;
using System.Collections.Generic;
using FirmwareBurner.Model.Project;
using FirmwarePacking;

namespace FirmwareBurner.Model
{
    /// <summary>Инструмент по формированию таблицы свойств в прошивке</summary>
    public interface IPropertiesTableGenerator
    {
        IDictionary<byte, int> Generate(TargetInformation Target, ModuleInformation Information, PackageInformation FirmwareInformation);
    }
}
