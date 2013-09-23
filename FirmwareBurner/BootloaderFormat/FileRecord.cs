using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwareBurner.BootloaderFormat
{
    public class FileRecord
    {
        public int FileAdress { get; set; }
        public int FileSize { get; set; }
        public uint Checksum { get; set; }
   }
}
