using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Binary.DataSections
{
    public interface ISectionContent<TMemoryKind>
    {
        void WriteTo(IWriter Writer);
    }
}
