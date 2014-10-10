using System.Collections.Generic;

namespace FirmwareBurner.Model.Images.Binary
{
    public class FirmwareImage : IBinaryImage
    {
        public List<FileRecord> FilesTable { get; set; }
        public List<ParamRecord> ParamList { get; set; }
        public BootloaderBody Bootloader { get; set; }

        public FirmwareImage()
        {
            this.FilesTable = new List<FileRecord>();
            this.ParamList = new List<ParamRecord>();
        }
    }
}
