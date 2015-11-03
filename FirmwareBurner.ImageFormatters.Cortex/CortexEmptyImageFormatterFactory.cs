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
        /// <param name="DeviceName">Тип устройства</param>
        public IImageFormatter<CortexImage> GetFormatter(string DeviceName)
        {
            return new CortexEmptyImageFormatter(new ImageFormatterInformation("Без загрузчика", new BootloaderApi(0, 0, int.MaxValue)),
                                                 _progressControllerFactory, _bufferFactory);
        }

        public ImageFormatterInformation Information { get; private set; }
    }
}
