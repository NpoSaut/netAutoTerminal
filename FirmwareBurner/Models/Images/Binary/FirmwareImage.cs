using System.Collections.Generic;

namespace FirmwareBurner.Models.Images.Binary
{
    public class FirmwareImage : IBinaryImage
    {
        public FirmwareImage()
        {
            FilesTable = new List<FileRecord>();
            ParamList = new List<ParamRecord>();
        }

        public List<FileRecord> FilesTable { get; set; }
        public List<ParamRecord> ParamList { get; set; }
        public BootloaderBody Bootloader { get; set; }
    }
}
