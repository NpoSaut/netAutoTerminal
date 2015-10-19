namespace FirmwareBurner.ImageFormatters.Binary
{
    public class Placement<TMemoryKind>
    {
        public Placement(TMemoryKind MemoryKind, int Address)
        {
            this.MemoryKind = MemoryKind;
            this.Address = Address;
        }

        public TMemoryKind MemoryKind { get; private set; }
        public int Address { get; private set; }
    }
}
