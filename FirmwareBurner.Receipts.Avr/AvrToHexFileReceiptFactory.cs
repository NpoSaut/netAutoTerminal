using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Imaging;
using FirmwareBurner.Receipts.Avr.BurnerFacades;

namespace FirmwareBurner.Receipts.Avr
{
    [BurningReceiptFactory("Сохранить в .hex файл")]
    [TargetDevice("at90can128"), TargetDevice("at90can64")]
    public class AvrToHexFileReceiptFactory : BurningReceiptFactoryBase<AvrImage>
    {
        private readonly IImageFormatterFactory<AvrImage> _imageFormatterFactory;
        private readonly AvrToHexFileToolFacadeFactory _toolFacadeFactory;

        public AvrToHexFileReceiptFactory(IImageFormatterFactory<AvrImage> ImageFormatterFactory, AvrToHexFileToolFacadeFactory ToolFacadeFactory)
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
