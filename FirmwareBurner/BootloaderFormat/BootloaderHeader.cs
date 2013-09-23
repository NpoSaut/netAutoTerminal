using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwareBurner.BootloaderFormat
{
    public class BootloaderHeader
    {
        public List<FileRecord> FileTable { get; set; }
        public List<ParamRecord> ParamList { get; set; }

        public BootloaderHeader()
        {
            this.FileTable = new List<FileRecord>();
            this.ParamList = new List<ParamRecord>();
        }
    }
}
