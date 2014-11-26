using System;
using System.Collections.Generic;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Project;
using FirmwarePacking;

namespace FirmwareBurner.Imaging
{
    public class PropertiesTableGenerator : IPropertiesTableGenerator
    {
        /// <summary>Формирует список FUDP-свойств, связанных с устройством</summary>
        /// <param name="Target">Целевое устройство</param>
        public IEnumerable<ParamRecord> GetDeviceProperties(TargetInformation Target)
        {
            return new List<ParamRecord>
                   {
                       // Информация о блоке
                       new ParamRecord(129, Target.CellId),
                       new ParamRecord(131, Target.SerialNumber),
                       new ParamRecord(132, Target.AssemblyDate.Year * 100 + Target.AssemblyDate.Month),
                       new ParamRecord(133, Target.ChannelNumber),
                       new ParamRecord(134, Target.ModificationId),
                   };
        }

        /// <summary>Формирует список FUDP-свойств, связанных с программным модулем</summary>
        /// <param name="Information">Информация о программном модуле</param>
        /// <param name="FirmwareInformation">Информация о прошивке</param>
        public IEnumerable<ParamRecord> GetModuleProperties(ModuleInformation Information, PackageInformation FirmwareInformation)
        {
            return new List<ParamRecord>
                   {
                       // Информация о модуле
                       new ParamRecord(130, Information.ModuleId),
                       
                       // Информация о прошивке
                       new ParamRecord(1, FirmwareInformation.FirmwareVersion.Major),
                       new ParamRecord(2, FirmwareInformation.FirmwareVersion.Minor),
                       new ParamRecord(3, (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds),
                   };
        }
    }
}
