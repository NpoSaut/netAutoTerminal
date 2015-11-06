using AsyncOperations.Progress;
using FirmwareBurner.Annotations;
using FirmwareBurner.ImageFormatters.Cortex.Catalog;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    [UsedImplicitly]
    public class CortexImageFormatterFactory : IImageFormatterFactory<CortexImage>
    {
        private readonly IBootloaderConfigurationCatalog _bootloaderConfigurationCatalog;
        private readonly CortexBootloaderInformation _bootloaderInformation;
        private readonly IBufferFactory _bufferFactory;
        private readonly IChecksumProvider _checksumProvider;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IStringEncoder _stringEncoder;

        public CortexImageFormatterFactory(ImageFormatterInformation Information, CortexBootloaderInformation BootloaderInformation,
                                           IProgressControllerFactory ProgressControllerFactory, IBufferFactory BufferFactory,
                                           IChecksumProvider ChecksumProvider, IStringEncoder StringEncoder,
                                           IBootloaderConfigurationCatalog BootloaderConfigurationCatalog)
        {
            _progressControllerFactory = ProgressControllerFactory;
            _bufferFactory = BufferFactory;
            _checksumProvider = ChecksumProvider;
            _stringEncoder = StringEncoder;
            _bootloaderConfigurationCatalog = BootloaderConfigurationCatalog;
            this.Information = Information;
            _bootloaderInformation = BootloaderInformation;
        }

        /// <summary>Создаёт сборщик образа для указанного типа устройства</summary>
        public IImageFormatter<CortexImage> GetFormatter()
        {
            return new CortexImageFormatter(_progressControllerFactory, _bufferFactory, _checksumProvider, _stringEncoder,
                                            _bootloaderConfigurationCatalog, _bootloaderInformation);
        }

        public ImageFormatterInformation Information { get; private set; }
    }
}
