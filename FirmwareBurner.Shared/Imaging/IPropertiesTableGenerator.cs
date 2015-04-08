using System.Collections.Generic;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Project;
using FirmwarePacking;

namespace FirmwareBurner.Imaging
{
    /// <summary>Инструмент по формированию таблицы FUDP-свойств в прошивке</summary>
    public interface IPropertiesTableGenerator
    {
        /// <summary>Формирует список FUDP-свойств, связанных с устройством</summary>
        /// <param name="Target">Целевое устройство</param>
        IEnumerable<ParamRecord> GetDeviceProperties(TargetInformation Target);

        /// <summary>Формирует список FUDP-свойств, связанных с программным модулем</summary>
        /// <param name="Project">Проект</param>
        IEnumerable<ParamRecord> GetModuleProperties(ModuleProject Project);
    }
}
