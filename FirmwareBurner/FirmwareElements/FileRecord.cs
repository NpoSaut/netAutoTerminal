using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FirmwareBurner.FirmwareElements
{
    public enum FileStorage { Flash, Eeprom }

    public class FileRecord
    {
        public FileStorage Placement { get; set; }
        public int FileAdress { get; set; }
        public Stream Body { get; set; }
        public int FileSize { get { return (int)Body.Length; } }
        public uint Checksum
        {
            get; set;
        }
   }
}
