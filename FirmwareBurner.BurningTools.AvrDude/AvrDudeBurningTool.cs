using System;
using System.Collections.Generic;
using System.IO;
using ExternalTools.Interfaces;
using FirmwareBurner.BurningTools.AvrDude.Parameters;

namespace FirmwareBurner.BurningTools.AvrDude
{
    public class AvrDudeBurningTool
    {
        private readonly String _chipName;
        private readonly IToolBody _toolBody;
        private readonly IToolLauncher _toolLauncher;

        public AvrDudeBurningTool(string ChipName, IToolBody ToolBody, IToolLauncher ToolLauncher)
        {
            _toolBody = ToolBody;
            _toolLauncher = ToolLauncher;
            _chipName = ChipName;
        }

        public void WriteFlash(FileInfo FlashFile)
        {
            LaunchAvrDude(AvrDudeMemoryType.Flash, AvrDudeMemoryOperationType.Write, FlashFile.FullName, AvrDudeInputFormat.IntelHex);
        }

        public void WriteEeprom(FileInfo EepromFile)
        {
            LaunchAvrDude(AvrDudeMemoryType.Eeprom, AvrDudeMemoryOperationType.Write, EepromFile.FullName, AvrDudeInputFormat.IntelHex);
        }

        public Fuses ReadFuse() { throw new NotImplementedException(); }

        public void WriteFuse(Fuses f)
        {
            var fuses = new Dictionary<AvrDudeMemoryType, byte>
                        {
                            { AvrDudeMemoryType.HFuse, f.FuseH },
                            { AvrDudeMemoryType.LFuse, f.FuseL },
                            { AvrDudeMemoryType.EFuse, f.FuseE }
                        };

            foreach (var fuse in fuses)
                LaunchAvrDude(fuse.Key, AvrDudeMemoryOperationType.Write, String.Format("0x{0:x2}", fuse.Value), AvrDudeInputFormat.IntelHex);
        }

        private string LaunchAvrDude(AvrDudeMemoryType MemoryType, AvrDudeMemoryOperationType Operation, string Input, AvrDudeInputFormat InputFormat)
        {
            _toolLauncher.Execute(_toolBody,
                                  new ConnectionAvrDudeParameter(AvrIspConnectionType.Usb),
                                  new ProgrammerIdAvrDudeParameter(ProgrammerType.AvrIsp),
                                  new ChipIdAvrDudeParameter(_chipName),
                                  new UCommandParameter(MemoryType,
                                                        Operation,
                                                        Input,
                                                        InputFormat));
        }
    }
}
