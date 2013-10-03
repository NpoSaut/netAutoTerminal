using System;
namespace FirmwareBurner.Burning.Burners.AvrIsp
{
    public interface IAvrIspCommandShell
    {
        string ChipName { get; set; }
        byte[] GetSignature();
        void WriteFlash(System.IO.FileInfo FlashFile, bool Erase = true);
        void WriteEeprom(System.IO.FileInfo EepromFile, bool Erase = true);
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
