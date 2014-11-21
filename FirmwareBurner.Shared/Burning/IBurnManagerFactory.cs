using FirmwareBurner.Imaging;

namespace FirmwareBurner.Burning
{
    /// <summary>Фабрика для изготовления <see cref="IBurnManager" />
    /// </summary>
    public interface IBurnManagerFactory
    {
        /// <summary>Имя изготавливаемого менеджера прошивок</summary>
        string BurnerName { get; }

        /// <summary>Тип устройства, которое может прошить изготавливаемый менеджер</summary>
        string BurningDeviceName { get; }

        /// <summary>Создаёт экземпляр <see cref="IBurnManager" />
        /// </summary>
        IBurnManager GetBurnManager();
    }

    public class BurnManagerFactory<TImage> : IBurnManagerFactory where TImage : IImage
    {
        private readonly IImageFormatter<TImage> _imageFormatter;
        private readonly IBurningReceipt<TImage> _receipt;

        public BurnManagerFactory(IImageFormatter<TImage> ImageFormatter, IBurningReceipt<TImage> Receipt)
        {
            _imageFormatter = ImageFormatter;
            _receipt = Receipt;
        }

        /// <summary>Имя изготавливаемого менеджера прошивок</summary>
        public string BurnerName
        {
            get { return _receipt.Name; }
        }

        /// <summary>Тип устройства, которое может прошить изготавливаемый менеджер</summary>
        public string BurningDeviceName { get; private set; }

        /// <summary>Создаёт экземпляр <see cref="IBurnManager" />
        /// </summary>
        public IBurnManager GetBurnManager() { return new ReceiptedBurnManager<TImage>(_imageFormatter, _receipt); }
    }
}
