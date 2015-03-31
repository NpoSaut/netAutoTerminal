using System;
using System.Collections.Generic;
using System.Linq;
using FirmwarePacking;

namespace FirmwareBurner.Project
{
    public class FirmwareProjectFactory : IFirmwareProjectFactory
    {
        /// <summary>Создаёт экземпляр проекта образа</summary>
        /// <param name="CellKindId">Идентификатор типа ячейки</param>
        /// <param name="CellModificationId">Идентификатор модификации ячейки</param>
        /// <param name="ChannelNumber">Номер канала</param>
        /// <param name="SerialNumber">Серийный номер ячейки</param>
        /// <param name="AssemblyDate">Дата производства ячейки</param>
        /// <param name="Modules">Сопоставление файлов прошивок программным модулям в ячейке</param>
        public FirmwareProject GetProject(int CellKindId, int CellModificationId, int ChannelNumber, int SerialNumber, DateTime AssemblyDate,
                                          ICollection<Tuple<int, FirmwarePackage>> Modules)
        {
            return
                new FirmwareProject(
                    new TargetInformation(ChannelNumber, CellKindId, CellModificationId, SerialNumber, AssemblyDate),
                    Modules.Select(module =>
                                   new ModuleProject(
                                       new ModuleInformation(module.Item1),
                                       module.Item2.Information,
                                       module.Item2.Components.Single(
                                           c => c.Targets.Any(t => t.CellId == CellKindId
                                                                   && t.CellModification == CellModificationId
                                                                   && t.Channel == ChannelNumber
                                                                   && t.Module == module.Item1)))).ToList());
        }
    }
}
