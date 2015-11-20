using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Binary.DataSections.Special
{
    public class ImageFileDataSection<TMemoryKind> : IDataSection<TMemoryKind>
    {
        private readonly BinaryImageFile<TMemoryKind> _file;
        public ImageFileDataSection(BinaryImageFile<TMemoryKind> File) { _file = File; }

        public Placement<TMemoryKind> Placement
        {
            get { return _file.Placement; }
        }

        public void WriteTo(IBuffer Buffer) { Buffer.Write(Placement.Address, _file.Content); }
    }
}