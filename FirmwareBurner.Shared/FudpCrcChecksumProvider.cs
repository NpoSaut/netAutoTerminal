namespace FirmwareBurner
{
    public class FudpCrcChecksumProvider : IChecksumProvider
    {
        public ushort GetChecksum(byte[] Data) { return FudpCrc.CalcCrc(Data); }
    }
}
