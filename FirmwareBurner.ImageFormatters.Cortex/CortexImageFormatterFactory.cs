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
        private readonly ICortexBootloaderInformationCatalog _bootloaderInformationCatalog;
        private readonly IBufferFactory _bufferFactory;
        private readonly IChecksumProvider _checksumProvider;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IStringEncoder _stringEncoder;

        public CortexImageFormatterFactory(IProgressControllerFactory ProgressControllerFactory, IBufferFactory BufferFactory,
                                           IChecksumProvider ChecksumProvider, IStringEncoder StringEncoder,
                                           ICortexBootloaderInformationCatalog BootloaderInformationCatalog,
                                           IBootloaderConfigurationCatalog BootloaderConfigurationCatalog)
        {
            _progressControllerFactory = ProgressControllerFactory;
            _bufferFactory = BufferFactory;
            _checksumProvider = ChecksumProvider;
            _stringEncoder = StringEncoder;
            _bootloaderInformationCatalog = BootloaderInformationCatalog;
            _bootloaderConfigurationCatalog = BootloaderConfigurationCatalog;
        }

        /// <summary>Создаёт сборщик образа для указанного типа устройства</summary>
        /// <param name="DeviceName">Тип устройства</param>
        public IImageFormatter<CortexImage> GetFormatter(string DeviceName)
        {
            return new CortexImageFormatter(_progressControllerFactory, _bufferFactory, _checksumProvider, _stringEncoder,
                                            _bootloaderConfigurationCatalog,
                                            _bootloaderInformationCatalog.GetBootloaderInformation(DeviceName));
        }
    }
}
