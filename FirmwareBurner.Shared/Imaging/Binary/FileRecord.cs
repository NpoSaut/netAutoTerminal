using System.IO;

namespace FirmwareBurner.Imaging.Binary
{
    public class FileRecord
    {
        public int FileAddress { get; set; }
        public Stream Body { get; set; }

        public int FileSize
        {
            get { return (int)Body.Length; }
        }

        public uint Checksum { get; set; }
    }
}
