using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Binary.DataSections
{
    public class StaticSectionContent<TMemoryKind> : ISectionContent<TMemoryKind>
    {
        private readonly byte[] _data;
        public StaticSectionContent(params byte[] Data) { _data = Data; }

        public void WriteTo(IWriter Writer) { Writer.WriteBytes(_data); }
    }
}
