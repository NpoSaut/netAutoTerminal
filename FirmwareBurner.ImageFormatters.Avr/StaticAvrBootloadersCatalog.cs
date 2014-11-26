using System.Collections.Generic;

namespace FirmwareBurner.ImageFormatters.Avr
{
    /// <summary>������������� ������� �����������</summary>
    public class StaticAvrBootloadersCatalog : IAvrBootloadersCatalog
    {
        private readonly IDictionary<string, AvrBootloaderInformation> _catalog =
            new Dictionary<string, AvrBootloaderInformation>
            {
                { "at90can128", GetDimaAvrBootloaderInformation(0x1e000) },
                { "at90can64", GetDimaAvrBootloaderInformation(0x0e000) }
            };

        /// <summary>��������� FUSE-����</summary>
        private static AvrFuses MagicFuses
        {
            get { return new AvrFuses(0xd8, 0xef, 0xfd); }
        }

        /// <summary>������� ���������� � ���������� ��� ���������� ����������</summary>
        /// <param name="DeviceName">�������� ���� ����������</param>
        public AvrBootloaderInformation GetBootloaderInformation(string DeviceName) { return _catalog[DeviceName]; }

        private static AvrBootloaderInformation GetDimaAvrBootloaderInformation(int bootloaderAddress)
        {
            var placements = new PlacementsInformation(bootloaderAddress, bootloaderAddress - 0x200, bootloaderAddress - 0x100);
            return new AvrBootloaderInformation(MagicFuses, placements);
        }
    }
}
