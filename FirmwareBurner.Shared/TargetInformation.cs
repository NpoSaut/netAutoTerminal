using System;

namespace FirmwareBurner
{
    /// <summary>Информация о цели прошивки</summary>
    public class TargetInformation : TargetHardwareIdentifier
    {
        public TargetInformation(int ChannelNumber, int CellId, int ModificationId, int SerialNumber, DateTime AssemblyDate)
            : base(ChannelNumber, CellId, ModificationId)
        {
            this.SerialNumber = SerialNumber;
            this.AssemblyDate = AssemblyDate;
        }

        /// <summary>Серийный номер ячейки</summary>
        public int SerialNumber { get; private set; }

        /// <summary>Дата производства ячейки</summary>
        public DateTime AssemblyDate { get; private set; }
    }
}
