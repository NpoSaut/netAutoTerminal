using System;

namespace FirmwareBurner.ImageFormatters.Binary.BodyLoaders
{
    public interface IBodyLoader
    {
        Byte[] LoadBootloaderBody();
    }
}
