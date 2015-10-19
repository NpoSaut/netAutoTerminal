using System;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;

namespace FirmwareBurner.ImageFormatters.Binary
{
    public abstract class BinaryBootloaderInformation<TMemoryKind>
    {
        private readonly Lazy<Byte[]> _body;

        protected BinaryBootloaderInformation(Placement<TMemoryKind> BootloaderPlacement, IBodyLoader BodyLoader)
        {
            this.BootloaderPlacement = BootloaderPlacement;
            _body = new Lazy<byte[]>(BodyLoader.LoadBootloaderBody);
        }

        public Placement<TMemoryKind> BootloaderPlacement { get; private set; }

        public byte[] GetBootloaderBody() { return _body.Value; }
    }
}
