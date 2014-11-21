using FirmwareBurner.Imaging;
using FirmwareBurner.Project;

namespace FirmwareBurner
{
    /// <summary>Менеджер прошивки</summary>
    /// <remarks>Занимается приготовлением образа прошивки и отправкой его получателю</remarks>
    public interface IBurnManager
    {
        /// <summary>Прошивает указанный проект</summary>
        /// <param name="Project">Проект для прожигания</param>
        void Burn(FirmwareProject Project);
    }

    /// <summary>Менеджер зашивки образа</summary>
    /// <typeparam name="TImage">Тип получаемого образа</typeparam>
    public class BurnManager<TImage> : IBurnManager where TImage : IImage
    {
        private readonly IBurningReceipt<TImage> _burningReceipt;
        private readonly IImageFormatter<TImage> _formatter;

        public BurnManager(IImageFormatter<TImage> Formatter, IBurningReceipt<TImage> BurningReceipt)
        {
            _formatter = Formatter;
            _burningReceipt = BurningReceipt;
        }

        /// <summary>Прошивает указанный проект</summary>
        /// <param name="Project">Проект для прожигания</param>
        public void Burn(FirmwareProject Project)
        {
            TImage image = _formatter.GetImage(Project);
            _burningReceipt.Burn(image);
        }
    }
}
