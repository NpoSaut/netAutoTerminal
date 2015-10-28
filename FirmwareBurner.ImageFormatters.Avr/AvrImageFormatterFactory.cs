using AsyncOperations.Progress;
using FirmwareBurner.Annotations;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Avr
{
    [UsedImplicitly]
    public class AvrImageFormatterFactory : IImageFormatterFactory<AvrImage>
    {
        private readonly IAvrBootloadersCatalog _bootloadersCatalog;
        private readonly IBufferFactory _bufferFactory;
        private readonly IAvrFileTableFormatter _filesTableFormatter;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IAvrPropertiesTableFormatter _propertiesTableFormatter;
        private readonly IPropertiesTableGenerator _propertiesTableGenerator;

        public AvrImageFormatterFactory(IPropertiesTableGenerator PropertiesTableGenerator, IAvrBootloadersCatalog BootloadersCatalog,
                                        IBufferFactory BufferFactory, IAvrFileTableFormatter FilesTableFormatter,
                                        IAvrPropertiesTableFormatter PropertiesTableFormatter, IProgressControllerFactory ProgressControllerFactory)
        {
            _propertiesTableGenerator = PropertiesTableGenerator;
            _bootloadersCatalog = BootloadersCatalog;
            _bufferFactory = BufferFactory;
            _filesTableFormatter = FilesTableFormatter;
            _propertiesTableFormatter = PropertiesTableFormatter;
            _progressControllerFactory = ProgressControllerFactory;
        }

        public IImageFormatter<AvrImage> GetFormatter(string DeviceName)
        {
            return new AvrImageFormatter(new ImageFormatterInformation("С загрузчиком", new BootloaderApi(1, 9, 1)),
                                         _bootloadersCatalog.GetBootloaderInformation(DeviceName), _propertiesTableGenerator, _bufferFactory,
                                         _filesTableFormatter, _propertiesTableFormatter, _progressControllerFactory);
        }
    }
}
