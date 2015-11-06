using AsyncOperations.Progress;
using FirmwareBurner.Annotations;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Avr
{
    [UsedImplicitly]
    public class AvrImageFormatterFactory : IImageFormatterFactory<AvrImage>
    {
        private readonly AvrBootloaderInformation _bootloaderInformation;
        private readonly IBufferFactory _bufferFactory;
        private readonly IAvrFileTableFormatter _filesTableFormatter;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IAvrPropertiesTableFormatter _propertiesTableFormatter;
        private readonly IPropertiesTableGenerator _propertiesTableGenerator;

        public AvrImageFormatterFactory(AvrBootloaderInformation BootloaderInformation,
                                        IPropertiesTableGenerator PropertiesTableGenerator, IBufferFactory BufferFactory,
                                        IAvrFileTableFormatter FilesTableFormatter, IAvrPropertiesTableFormatter PropertiesTableFormatter,
                                        IProgressControllerFactory ProgressControllerFactory)
        {
            _propertiesTableGenerator = PropertiesTableGenerator;
            _bufferFactory = BufferFactory;
            _filesTableFormatter = FilesTableFormatter;
            _propertiesTableFormatter = PropertiesTableFormatter;
            _progressControllerFactory = ProgressControllerFactory;
            _bootloaderInformation = BootloaderInformation;
            Information = new ImageFormatterInformation("С загрузчиком", new BootloaderApi(1, 9, 1));
        }

        public IImageFormatter<AvrImage> GetFormatter()
        {
            return new AvrImageFormatter(_bootloaderInformation, _propertiesTableGenerator, _bufferFactory,
                                         _filesTableFormatter, _propertiesTableFormatter, _progressControllerFactory);
        }

        public ImageFormatterInformation Information { get; private set; }
    }
}
