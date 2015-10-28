using AsyncOperations.Progress;
using FirmwareBurner.Burning;
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
        }

        /// <summary>Создаёт сборщик образа для указанного типа устройства</summary>
        /// <param name="DeviceName">Тип устройства</param>
        public IImageFormatter<CortexImage> GetFormatter(string DeviceName)
        {
            return new CortexEmptyImageFormatter(_progressControllerFactory, _bufferFactory);
        }
    }
}
