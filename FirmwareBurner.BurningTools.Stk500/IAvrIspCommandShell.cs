using System.IO;

namespace FirmwareBurner.BurningTools.Stk500
{
    public interface IAvrIspCommandShell
    {
        string ChipName { get; set; }
        byte[] GetSignature();
        void WriteFlash(FileInfo FlashFile, bool Erase = true);
        void WriteEeprom(FileInfo EepromFile, bool Erase = true);
        Fuses ReadFuse();
        void WriteFuse(Fuses f);
    }

    public struct Fuses
    {
        public byte FuseL { get; set; }
        public byte FuseH { get; set; }
        public byte FuseE { get; set; }
    }
}
