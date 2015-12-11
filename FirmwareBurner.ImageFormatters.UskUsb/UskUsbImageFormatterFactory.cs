using System.Collections.Generic;
using AsyncOperations.Progress;
using FirmwareBurner.ImageFormatters.Binary.FileParsers;
using FirmwareBurner.ImageFormatters.Cortex;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.UskUsb
{
    public class UskUsbImageFormatterFactory : IImageFormatterFactory<CortexImage>
    {
        private static readonly IDictionary<string, CortexMemoryKind> _memoryKinds =
            new Dictionary<string, CortexMemoryKind>
            {
                { "f", CortexMemoryKind.Flash }
            };

        private readonly UskUsbBootloaderInformation _bootloaderInformation;
        private readonly IBufferFactory _bufferFactory;
        private readonly IChecksumProvider _checksumProvider;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IStringEncoder _stringEncoder;

        public UskUsbImageFormatterFactory(UskUsbBootloaderInformation BootloaderInformation, IProgressControllerFactory ProgressControllerFactory,
                                           IBufferFactory BufferFactory, IChecksumProvider ChecksumProvider, IStringEncoder StringEncoder)
        {
            _progressControllerFactory = ProgressControllerFactory;
            _bootloaderInformation = BootloaderInformation;
            _checksumProvider = ChecksumProvider;
            _stringEncoder = StringEncoder;
            _bufferFactory = BufferFactory;
            Information = new ImageFormatterInformation("С USB-загрузчиком", _bootloaderInformation.Api);
        }

        /// <summary>Создаёт сборщик образа для указанного типа устройства</summary>
        public IImageFormatter<CortexImage> GetFormatter()
        {
            return new UskUsbImageFormatter(_progressControllerFactory, _bufferFactory, _bootloaderInformation,
                                            new DoubleLayerFileParser<CortexMemoryKind>(_memoryKinds), _checksumProvider, _stringEncoder);
        }

        public ImageFormatterInformation Information { get; private set; }
    }
}
