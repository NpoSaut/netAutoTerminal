using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    public class CortexImage : IBinaryImage
    {
        public CortexImage(IBuffer FlashBuffer) { this.FlashBuffer = FlashBuffer; }

        /// <summary>Содержимое Flash-памяти</summary>
        public IBuffer FlashBuffer { get; private set; }
    }
}
