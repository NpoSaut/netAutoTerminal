using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Attributes;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.Burning
{
    /// <summary>Фабрика для изготовления <see cref="IBurningReceipt" />
    /// </summary>
    public interface IBurningReceiptFactory
    {
        /// <summary>Имя изготавливаемого менеджера прошивок</summary>
        string BurnerName { get; }

        IEnumerable<string> TargetDevices { get; }

        /// <summary>Создаёт экземпляр <see cref="IBurningReceipt" />
        /// </summary>
        /// <param name="DeviceName"></param>
        IBurningReceipt GetBurnManager(string DeviceName);
    }

    public abstract class BurningReceiptFactory<TImage> : IBurningReceiptFactory where TImage : IImage
    {
        private readonly IImageFormatterFactory<TImage> _imageFormatterFactory;
        private readonly IBurningToolFacadeFactory<TImage> _toolFacadeFactory;

        public BurningReceiptFactory(IImageFormatterFactory<TImage> ImageFormatterFactory, IBurningToolFacadeFactory<TImage> ToolFacadeFactory)
        {
            _imageFormatterFactory = ImageFormatterFactory;
            _toolFacadeFactory = ToolFacadeFactory;
        }

        /// <summary>Имя изготавливаемого менеджера прошивок</summary>
        public string BurnerName
        {
            // todo: сделать
            get { return "блаблаблаблабла " + _toolFacadeFactory; }
        }

        public IEnumerable<string> TargetDevices
        {
            get
            {
                return _toolFacadeFactory.GetTargetDeviceNames();
            }
        }

        /// <summary>Создаёт экземпляр <see cref="IBurningReceipt" />
        /// </summary>
        /// <param name="DeviceName"></param>
        public IBurningReceipt GetBurnManager(string DeviceName)
        {
            return new BurningReceipt<TImage>(_imageFormatterFactory.GetFormatter(DeviceName), _toolFacadeFactory.GetBurningToolFacade(DeviceName), DeviceName);
        }
    }
}
