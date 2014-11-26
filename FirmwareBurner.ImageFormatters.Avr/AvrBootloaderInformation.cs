using FirmwareBurner.Annotations;

namespace FirmwareBurner.ImageFormatters.Avr
{
    public class AvrBootloaderInformation
    {
        /// <summary>�������������� ����� ��������� ������ <see cref="T:System.Object" />.</summary>
        public AvrBootloaderInformation(AvrFuses RequiredFuses, PlacementsInformation Placements)
        {
            this.Placements = Placements;
            this.RequiredFuses = RequiredFuses;
        }

        /// <summary>���������� �������� � �������� ��������</summary>
        public PlacementsInformation Placements { get; private set; }

        /// <summary>����������� �������� FUSE-�����</summary>
        public AvrFuses RequiredFuses { get; private set; }

        /// <summary>�������� ���� ����������</summary>
        [NotNull]
        public byte[] GetBootloaderBody()
        {
            // TODO: Return bootloader body!!!
            return new byte[10];
        }
    }
}
