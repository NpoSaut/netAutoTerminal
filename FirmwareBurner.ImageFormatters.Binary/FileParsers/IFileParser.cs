using FirmwarePacking;

namespace FirmwareBurner.ImageFormatters.Binary.FileParsers
{
    public interface IFileParser<TMemoryKind>
    {
        BinaryImageFile<TMemoryKind> GetImageFile(FirmwareFile FirmwareFile);
    }
}