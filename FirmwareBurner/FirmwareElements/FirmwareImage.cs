using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwareBurner.FirmwareElements
{
    public class FirmwareImage
    {
        public List<FileRecord> FileTable { get; set; }
        public List<ParamRecord> ParamList { get; set; }
        public BootloaderBody Bootloader { get; set; }

        public FirmwareImage()
        {
            this.FileTable = new List<FileRecord>();
            this.ParamList = new List<ParamRecord>();
        }
    }
}
