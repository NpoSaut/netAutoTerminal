using System.Collections.Generic;
using System.Linq;
using AsyncOperations.Progress;
using FirmwareBurner.ImageFormatters.Avr.Catalog;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Avr
{
    public class AvrImageFormatterCatalog : EnumerableImageFormattersCatalogBase<AvrImage>
    {
        private readonly IAvrBootloadersCatalog _bootloadersCatalog;
        private readonly IBufferFactory _bufferFactory;
        private readonly IChecksumProvider _checksumProvider;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IStringEncoder _stringEncoder;

        public AvrImageFormatterCatalog(IAvrBootloadersCatalog BootloadersCatalog, IBufferFactory BufferFactory, IChecksumProvider ChecksumProvider,
                                        IProgressControllerFactory ProgressControllerFactory, IStringEncoder StringEncoder)
        {
            _bootloadersCatalog = BootloadersCatalog;
            _bufferFactory = BufferFactory;
            _checksumProvider = ChecksumProvider;
            _progressControllerFactory = ProgressControllerFactory;
            _stringEncoder = StringEncoder;
        }

        protected override IEnumerable<Fac> EnumerateFactories()
        {
            return _bootloadersCatalog.GetBootloaderInformations().Select(i => new Fac(i.DeviceName, CreateFactory(i)));
        }

        private IImageFormatterFactory<AvrImage> CreateFactory(AvrBootloaderInformation BootloaderInformation)
        {
            return new AvrImageFormatterFactory(BootloaderInformation, _bufferFactory, _progressControllerFactory, _checksumProvider, _stringEncoder);
        }
    }
}
