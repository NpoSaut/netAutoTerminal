namespace FirmwareBurner
{
    /// <summary>Идентификатор цели прошивки</summary>
    public class TargetHardwareIdentifier
    {
        public TargetHardwareIdentifier(int ChannelNumber, int CellId, int ModificationId)
        {
            this.ChannelNumber = ChannelNumber;
            this.CellId = CellId;
            this.ModificationId = ModificationId;
        }

        /// <summary>Идентификатор ячейки</summary>
        public int CellId { get; private set; }

        /// <summary>Идентификатор модификации ячейки</summary>
        public int ModificationId { get; private set; }

        /// <summary>Номер полукомплекта</summary>
        public int ChannelNumber { get; private set; }
    }
}