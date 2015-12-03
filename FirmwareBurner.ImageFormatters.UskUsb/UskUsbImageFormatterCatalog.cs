using System.Collections.Generic;
using System.Linq;
using AsyncOperations.Progress;
using FirmwareBurner.ImageFormatters.Cortex;
using FirmwareBurner.ImageFormatters.UskUsb.Catalog;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.UskUsb
{
    public class UskUsbImageFormatterCatalog : EnumerableImageFormattersCatalogBase<CortexImage>
    {
        private readonly IUskUsbBootloadersCatalog _bootloadersCatalog;
        private readonly IBufferFactory _bufferFactory;
        private readonly IChecksumProvider _checksumProvider;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IStringEncoder _stringEncoder;

        public UskUsbImageFormatterCatalog(IUskUsbBootloadersCatalog BootloadersCatalog, IProgressControllerFactory ProgressControllerFactory,
                                           IBufferFactory BufferFactory, IChecksumProvider ChecksumProvider, IStringEncoder StringEncoder)
        {
            _bootloadersCatalog = BootloadersCatalog;
            _progressControllerFactory = ProgressControllerFactory;
            _bufferFactory = BufferFactory;
            _checksumProvider = ChecksumProvider;
            _stringEncoder = StringEncoder;
        }

        protected override IEnumerable<Fac> EnumerateFactories()
        {
            return _bootloadersCatalog.GetBootloaderInformations()
                                      .Select(i => new Fac(i.DeviceName, CreateFactory(i)));
        }

        private IImageFormatterFactory<CortexImage> CreateFactory(UskUsbBootloaderInformation BootloaderInformation)
        {
            return new UskUsbImageFormatterFactory(BootloaderInformation, _progressControllerFactory, _bufferFactory, _checksumProvider, _stringEncoder);
        }
    }
}
