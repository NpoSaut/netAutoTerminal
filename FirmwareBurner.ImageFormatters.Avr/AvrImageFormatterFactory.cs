using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Avr
{
    public class AvrImageFormatterFactory : IImageFormatterFactory<AvrImage>
    {
        private readonly IAvrBootloadersCatalog _bootloadersCatalog;
        private readonly IBufferFactory _bufferFactory;
        private readonly IBinaryFileTableFormatter _filesTableFormatter;
        private readonly IBinaryPropertiesTableFormatter _propertiesTableFormatter;
        private readonly IPropertiesTableGenerator _propertiesTableGenerator;

        public AvrImageFormatterFactory(IPropertiesTableGenerator PropertiesTableGenerator, IAvrBootloadersCatalog BootloadersCatalog,
                                        IBufferFactory BufferFactory, IBinaryFileTableFormatter FilesTableFormatter,
                                        IBinaryPropertiesTableFormatter PropertiesTableFormatter)
        {
            _propertiesTableGenerator = PropertiesTableGenerator;
            _bootloadersCatalog = BootloadersCatalog;
            _bufferFactory = BufferFactory;
            _filesTableFormatter = FilesTableFormatter;
            _propertiesTableFormatter = PropertiesTableFormatter;
        }

        public IImageFormatter<AvrImage> GetFormatter(string DeviceName)
        {
            return new AvrImageFormatter(_bootloadersCatalog.GetBootloaderInformation(DeviceName), _propertiesTableGenerator, _bufferFactory,
                                         _filesTableFormatter, _propertiesTableFormatter);
        }
    }
}
