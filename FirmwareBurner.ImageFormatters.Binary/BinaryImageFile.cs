using System;

namespace FirmwareBurner.ImageFormatters.Binary
{
    public class BinaryImageFile<TMemoryKind>
    {
        public BinaryImageFile(Placement<TMemoryKind> Placement, byte[] Content)
        {
            this.Placement = Placement;
            this.Content = Content;
        }

        public Placement<TMemoryKind> Placement { get; private set; }
        public Byte[] Content { get; private set; }
    }
}
