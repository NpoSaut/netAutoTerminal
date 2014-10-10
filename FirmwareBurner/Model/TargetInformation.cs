using System;

namespace FirmwareBurner.Model
{
    /// <summary>Информация о цели прошивки</summary>
    public class TargetInformation
    {
        /// <summary>Серийный номер ячейки</summary>
        public int SerialNumber { get; private set; }

        /// <summary>Номер полукомплекта</summary>
        public int HalfsetNumber { get; private set; }

        /// <summary>Идентификатор ячейки</summary>
        public int CellId { get; private set; }

        /// <summary>Идентификатор модификации ячейки</summary>
        public int ModificationId { get; private set; }

        /// <summary>Дата производства ячейки</summary>
        public DateTime AssemblyDate { get; private set; }
    }
}
