using System.Collections.Generic;
using AsyncOperations.Progress;
using FirmwareBurner.Annotations;
using FirmwareBurner.ImageFormatters.Avr.Catalog;
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

        private readonly IAvrBootloadersCatalog _bootloadersCatalog;

        private readonly IBufferFactory _bufferFactory;
        private readonly IChecksumProvider _checksumProvider;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IStringEncoder _stringEncoder;

        public AvrImageFormatterFactory(IAvrBootloadersCatalog BootloadersCatalog, IBufferFactory BufferFactory,
                                        IProgressControllerFactory ProgressControllerFactory, IChecksumProvider ChecksumProvider, IStringEncoder StringEncoder)
        {
            _bootloadersCatalog = BootloadersCatalog;
            _bufferFactory = BufferFactory;
            _progressControllerFactory = ProgressControllerFactory;
            _checksumProvider = ChecksumProvider;
            _stringEncoder = StringEncoder;
        }

        /// <summary>Создаёт сборщик образа для указанного типа устройства</summary>
        /// <param name="DeviceName">Тип устройства</param>
        public IImageFormatter<AvrImage> GetFormatter(string DeviceName)
        {
            return new AvrImageFormatter(_progressControllerFactory, _bufferFactory, _bootloadersCatalog.GetBootloaderInformation(DeviceName),
                                         new DoubleLayerFileParser<AvrMemoryKind>(_memoryKinds), _checksumProvider, _stringEncoder);
        }
    }
}
