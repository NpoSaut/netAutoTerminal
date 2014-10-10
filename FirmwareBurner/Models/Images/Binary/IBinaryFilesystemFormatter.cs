using System.IO;
using FirmwareBurner.Implementations.Avr;

namespace FirmwareBurner.Model.Images.Binary
{
    public interface IBinaryFilesystemFormatter
    {
        void AddFile(Stream DestinationStream, ImageFile File);
    }
}