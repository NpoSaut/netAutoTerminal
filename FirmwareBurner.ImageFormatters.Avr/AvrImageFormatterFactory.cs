using System.Collections.Generic;
using AsyncOperations.Progress;
using FirmwareBurner.Annotations;
using FirmwareBurner.ImageFormatters.Binary.FileParsers;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Avr
{
    [UsedImplicitly]
    public class AvrImageFormatterFactory : IImageFormatterFactory<AvrImage>
    {
        private static readonly IDictionary<string, AvrMemoryKind> _memoryKinds =
            new Dictionary<string, AvrMemoryKind>
            {
                { "f", AvrMemoryKind.Flash },
                { "e", AvrMemoryKind.Eeprom }
            };

        private readonly AvrBootloaderInformation _bootloaderInformation;
        private readonly IBufferFactory _bufferFactory;
        private readonly IChecksumProvider _checksumProvider;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IStringEncoder _stringEncoder;

        public AvrImageFormatterFactory(AvrBootloaderInformation BootloaderInformation, IBufferFactory BufferFactory,
                                        IProgressControllerFactory ProgressControllerFactory, IChecksumProvider ChecksumProvider, IStringEncoder StringEncoder)
        {
            _bufferFactory = BufferFactory;
            _progressControllerFactory = ProgressControllerFactory;
            _checksumProvider = ChecksumProvider;
            _bootloaderInformation = BootloaderInformation;
            _stringEncoder = StringEncoder;
            Information = new ImageFormatterInformation("С загрузчиком", new BootloaderApi(1, 9, 1));
        }

        public IImageFormatter<AvrImage> GetFormatter()
        {
            return new AvrImageFormatter(_progressControllerFactory, _bufferFactory, _bootloaderInformation,
                                         new DoubleLayerFileParser<AvrMemoryKind>(_memoryKinds), _checksumProvider, _stringEncoder);
        }

        public ImageFormatterInformation Information { get; private set; }
    }
}
