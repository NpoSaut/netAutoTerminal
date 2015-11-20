using System;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Binary
{
    public abstract class BinaryBootloaderInformation<TMemoryKind>
    {
        private readonly Lazy<Byte[]> _body;

        protected BinaryBootloaderInformation(string DeviceName, Placement<TMemoryKind> BootloaderPlacement, IBodyLoader BodyLoader, BootloaderApi Api)
        {
            this.Api = Api;
            this.DeviceName = DeviceName;
            this.BootloaderPlacement = BootloaderPlacement;
            _body = new Lazy<byte[]>(BodyLoader.LoadBootloaderBody);
        }

        public string DeviceName { get; private set; }
        public Placement<TMemoryKind> BootloaderPlacement { get; private set; }
        public BootloaderApi Api { get; private set; }

        public byte[] GetBootloaderBody() { return _body.Value; }
    }
}
