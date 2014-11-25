using FirmwareBurner.Annotations;
using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Imaging;
using FirmwareBurner.Receipts.Avr.BurnerFacades;

namespace FirmwareBurner.Receipts.Avr
{
    [BurningReceiptFactory("Через STK500")]
    [TargetDevice("at90can128"), TargetDevice("at90can64")]
    public class AvrOverStk500ReceiptFactory : BurningReceiptFactoryBase<AvrImage>
    {
        private readonly IImageFormatterFactory<AvrImage> _imageFormatterFactory;
        private readonly AvrOverStk500BurningToolFacadeFactory _toolFacadeFactory;

        public AvrOverStk500ReceiptFactory(IImageFormatterFactory<AvrImage> ImageFormatterFactory, AvrOverStk500BurningToolFacadeFactory ToolFacadeFactory)
        {
            _imageFormatterFactory = ImageFormatterFactory;
            _toolFacadeFactory = ToolFacadeFactory;
        }

        /// <summary>Создаёт экземпляр <see cref="IBurningReceipt" />, пригодный для прошивания указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        public override IBurningReceipt GetReceipt(string DeviceName)
        {
            return new BurningReceipt<AvrImage>(_imageFormatterFactory.GetFormatter(DeviceName),
                                                _toolFacadeFactory.GetBurningToolFacade(DeviceName));
        }
    }
}
