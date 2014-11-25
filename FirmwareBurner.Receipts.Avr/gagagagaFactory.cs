using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.Receipts.Avr
{
    public class gagagagaFactory : BurningReceiptFactory<AvrImage>
    {
        public gagagagaFactory(IImageFormatterFactory<AvrImage> ImageFormatterFactory, IBurningToolFacadeFactory<AvrImage> ToolFacadeFactory) : base(ImageFormatterFactory, ToolFacadeFactory) { }
    }
}