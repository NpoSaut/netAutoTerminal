using System.IO;

namespace FirmwareBurner.Imaging.Binary
{
    public interface IBinaryFilesystemFormatter
    {
        void AddFile(Stream DestinationStream, ImageFile File);
    }
}
