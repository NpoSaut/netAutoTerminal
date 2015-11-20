using System.Collections.Generic;
using System.Linq;
using AsyncOperations.Progress;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwarePacking;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    public class CortexEmptyImageFormatterCatalog : IImageFormattersCatalog<CortexImage>
    {
        private readonly IBufferFactory _bufferFactory;
        private readonly IProgressControllerFactory _progressControllerFactory;

        public CortexEmptyImageFormatterCatalog(IBufferFactory BufferFactory, IProgressControllerFactory ProgressControllerFactory)
        {
            _bufferFactory = BufferFactory;
            _progressControllerFactory = ProgressControllerFactory;
        }

        public IEnumerable<IImageFormatterFactory<CortexImage>> GetAppropriateFormatterFactories(string DeviceName, IList<BootloaderRequirement> Requirements)
        {
            if (Requirements.Where(r => r != null).All(r => r.BootloaderId == 0))
                yield return new CortexEmptyImageFormatterFactory(_bufferFactory, _progressControllerFactory);
        }
    }
}
