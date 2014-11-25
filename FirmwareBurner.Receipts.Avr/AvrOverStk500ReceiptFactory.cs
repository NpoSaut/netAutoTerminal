using FirmwareBurner.Annotations;
using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.Receipts.Avr
{
    [UsedImplicitly]
    public class AvrOverStk500ReceiptFactory : BurningReceiptFactoryBase<AvrImage>
    {
        public AvrOverStk500ReceiptFactory(IImageFormatterFactory<AvrImage> ImageFormatterFactory, IBurningToolFacadeFactory<AvrImage> ToolFacadeFactory)
            : base(ImageFormatterFactory, ToolFacadeFactory) { }
    }
}
