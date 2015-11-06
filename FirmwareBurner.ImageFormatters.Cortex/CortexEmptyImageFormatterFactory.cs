using AsyncOperations.Progress;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    public class CortexEmptyImageFormatterFactory : IImageFormatterFactory<CortexImage>
    {
        private readonly IBufferFactory _bufferFactory;
        private readonly IProgressControllerFactory _progressControllerFactory;

        public CortexEmptyImageFormatterFactory(IBufferFactory BufferFactory, IProgressControllerFactory ProgressControllerFactory)
        {
            _bufferFactory = BufferFactory;
            _progressControllerFactory = ProgressControllerFactory;
            Information = new ImageFormatterInformation("Без загрузчика", BootloaderApi.Empty);
        }

        /// <summary>Создаёт сборщик образа для указанного типа устройства</summary>
        public IImageFormatter<CortexImage> GetFormatter() { return new CortexEmptyImageFormatter(_progressControllerFactory, _bufferFactory); }

        public ImageFormatterInformation Information { get; private set; }
    }
}
