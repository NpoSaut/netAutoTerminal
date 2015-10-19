using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Binary.DataSections
{
    public interface IDataSection<TMemoryKind>
    {
        Placement<TMemoryKind> Placement { get; }
        void WriteTo(IBuffer Buffer);
    }
}