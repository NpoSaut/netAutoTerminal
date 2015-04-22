using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Project;

namespace FirmwareBurner.Imaging
{
    public class PropertiesTableGenerator : IPropertiesTableGenerator
    {
        private readonly IStringEncoder _stringEncoder;
        public PropertiesTableGenerator(IStringEncoder StringEncoder) { _stringEncoder = StringEncoder; }

        /// <summary>Формирует список FUDP-свойств, связанных с устройством</summary>
        /// <param name="Project">Проект</param>
        /// <param name="ModuleId">Идентификатор модуля</param>
        public IEnumerable<ParamRecord> GetDeviceProperties(FirmwareProject Project, int ModuleId)
        {
            return new List<ParamRecord>
                   {
                       // Информация о блоке
                       new ParamRecord(129, Project.Target.CellId),
                       new ParamRecord(131, Project.Target.SerialNumber),
                       new ParamRecord(132, Project.Target.AssemblyDate.Year * 100 + Project.Target.AssemblyDate.Month),
                       new ParamRecord(133, Project.Target.ChannelNumber),
                       new ParamRecord(134, Project.Target.ModificationId),
                       new ParamRecord(130, ModuleId),
                   };
        }

        /// <summary>Формирует список FUDP-свойств, связанных с программным модулем</summary>
        /// <param name="Project">Проект</param>
        public IEnumerable<ParamRecord> GetModuleProperties(ModuleProject Project)
        {
            return new List<ParamRecord>
                   {
                       // Информация о прошивке
                       new ParamRecord(1, Project.FirmwareInformation.FirmwareVersion.Major),
                       new ParamRecord(2, Project.FirmwareInformation.FirmwareVersion.Minor),
                       new ParamRecord(3, (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds),
                       new ParamRecord(6, Project.FirmwareContent.Files.Select(f => FudpCrc.CalcCrc(f.Content)).Aggregate((res, fcs) => (ushort)(res ^ fcs))),
                       new ParamRecord(7, _stringEncoder.Encode(Project.FirmwareInformation.FirmwareVersionLabel))
                   };
        }
    }
}
