using System;

namespace FirmwareBurner.Models
{
    /// <summary>Информация о цели прошивки</summary>
    public class TargetInformation
    {
        public TargetInformation(int ChannelNumber, int CellId, int ModificationId, int SerialNumber, DateTime AssemblyDate)
        {
            this.SerialNumber = SerialNumber;
            this.ChannelNumber = ChannelNumber;
            this.CellId = CellId;
            this.ModificationId = ModificationId;
            this.AssemblyDate = AssemblyDate;
        }

        /// <summary>Серийный номер ячейки</summary>
        public int SerialNumber { get; private set; }

        /// <summary>Номер полукомплекта</summary>
        public int ChannelNumber { get; private set; }

        /// <summary>Идентификатор ячейки</summary>
        public int CellId { get; private set; }

        /// <summary>Идентификатор модификации ячейки</summary>
        public int ModificationId { get; private set; }

        /// <summary>Дата производства ячейки</summary>
        public DateTime AssemblyDate { get; private set; }
    }
}
