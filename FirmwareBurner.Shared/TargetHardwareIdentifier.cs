namespace FirmwareBurner
{
    /// <summary>������������� ���� ��������</summary>
    public class TargetHardwareIdentifier
    {
        public TargetHardwareIdentifier(int ChannelNumber, int CellId, int ModificationId)
        {
            this.ChannelNumber = ChannelNumber;
            this.CellId = CellId;
            this.ModificationId = ModificationId;
        }

        /// <summary>������������� ������</summary>
        public int CellId { get; private set; }

        /// <summary>������������� ����������� ������</summary>
        public int ModificationId { get; private set; }

        /// <summary>����� �������������</summary>
        public int ChannelNumber { get; private set; }
    }
}