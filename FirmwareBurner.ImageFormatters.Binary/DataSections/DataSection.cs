using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Binary.DataSections
{
    public class DataSection<TMemoryKind> : IDataSection<TMemoryKind>
    {
        public DataSection(Placement<TMemoryKind> Placement, ISectionContent<TMemoryKind> Content)
        {
            this.Placement = Placement;
            this.Content = Content;
        }

        public ISectionContent<TMemoryKind> Content { get; private set; }
        public Placement<TMemoryKind> Placement { get; private set; }

        public void WriteTo(IBuffer Buffer)
        {
            var bufferWriter = new BufferWriter(Buffer, Placement.Address);
            Content.WriteTo(bufferWriter);
        }
    }
}
