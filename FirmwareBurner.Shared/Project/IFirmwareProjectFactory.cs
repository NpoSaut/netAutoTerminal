using System;
using System.Collections.Generic;
using FirmwarePacking;

namespace FirmwareBurner.Project
{
    public interface IFirmwareProjectFactory
    {
        /// <summary>Создаёт экземпляр проекта образа</summary>
        /// <param name="CellKindId">Идентификатор типа ячейки</param>
        /// <param name="CellModificationId">Идентификатор модификации ячейки</param>
        /// <param name="ChannelNumber">Номер канала</param>
        /// <param name="SerialNumber">Серийный номер ячейки</param>
        /// <param name="AssemblyDate">Дата производства ячейки</param>
        /// <param name="Modules">Сопоставление файлов прошивок программным модулям в ячейке</param>
        FirmwareProject GetProject(int CellKindId, int CellModificationId, int ChannelNumber, int SerialNumber, DateTime AssemblyDate,
                                   ICollection<Tuple<int, FirmwarePackage>> Modules);
    }
}
