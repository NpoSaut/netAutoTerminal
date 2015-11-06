using System.Collections.Generic;
using System.Linq;
using AsyncOperations.Progress;
using FirmwareBurner.ImageFormatters.Cortex.Catalog;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    public class CortexImageFormatterCatalog : EnumerableImageFormattersCatalogBase<CortexImage>
    {
        private readonly IBootloaderConfigurationCatalog _bootloaderConfigurationCatalog;
        private readonly ICortexBootloaderInformationCatalog _bootloaderInformationCatalog;
        private readonly IBufferFactory _bufferFactory;
        private readonly IChecksumProvider _checksumProvider;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IStringEncoder _stringEncoder;

        public CortexImageFormatterCatalog(ICortexBootloaderInformationCatalog BootloaderInformationCatalog,
                                           IBootloaderConfigurationCatalog BootloaderConfigurationCatalog, IBufferFactory BufferFactory,
                                           IChecksumProvider ChecksumProvider, IStringEncoder StringEncoder,
                                           IProgressControllerFactory ProgressControllerFactory)
        {
            _bootloaderInformationCatalog = BootloaderInformationCatalog;
            _bootloaderConfigurationCatalog = BootloaderConfigurationCatalog;
            _bufferFactory = BufferFactory;
            _checksumProvider = ChecksumProvider;
            _stringEncoder = StringEncoder;
            _progressControllerFactory = ProgressControllerFactory;
        }

        protected override IEnumerable<Fac> EnumerateFactories()
        {
            return _bootloaderInformationCatalog.GetBootloaderInformations()
                                                .Select(i => new Fac(i.DeviceName,
                                                                     CreateFactory(new ImageFormatterInformation("С загрузчиком", i.Api), i)));
        }

        private CortexImageFormatterFactory CreateFactory(ImageFormatterInformation ImageFormatterInformation, CortexBootloaderInformation i)
        {
            return new CortexImageFormatterFactory(ImageFormatterInformation, i, _progressControllerFactory, _bufferFactory, _checksumProvider, _stringEncoder,
                                                   _bootloaderConfigurationCatalog);
        }
    }
}
