using System.Collections.Generic;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.Burning
{
    /// <summary>Фабрика, изготавливающая рецепты прошивки типа ImageToTool (<see cref="BurningReceipt{TImage}" />)</summary>
    /// <typeparam name="TImage">Тип создаваемого образа</typeparam>
    public abstract class BurningReceiptFactory<TImage> : IBurningReceiptFactory where TImage : IImage
    {
        private readonly IImageFormatterFactory<TImage> _imageFormatterFactory;
        private readonly IBurningToolFacadeFactory<TImage> _toolFacadeFactory;

        public BurningReceiptFactory(IImageFormatterFactory<TImage> ImageFormatterFactory, IBurningToolFacadeFactory<TImage> ToolFacadeFactory)
        {
            _imageFormatterFactory = ImageFormatterFactory;
            _toolFacadeFactory = ToolFacadeFactory;
        }

        /// <summary>Имя изготавливаемого рецепта</summary>
        public string ReceiptName
        {
            // todo: сделать
            get { return "блаблаблаблабла " + _toolFacadeFactory; }
        }

        /// <summary>Типы устройств, для которых может использоваться этот рецепт</summary>
        public IEnumerable<string> TargetDevices
        {
            get { return _toolFacadeFactory.GetTargetDeviceNames(); }
        }

        /// <summary>Создаёт экземпляр <see cref="IBurningReceipt" />, пригодный для прошивания указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        public IBurningReceipt GetBurnManager(string DeviceName)
        {
            return new BurningReceipt<TImage>(_imageFormatterFactory.GetFormatter(DeviceName), _toolFacadeFactory.GetBurningToolFacade(DeviceName));
        }
    }
}
