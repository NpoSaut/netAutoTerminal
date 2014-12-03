using FirmwareBurner.Imaging;
using FirmwareBurner.Project;

namespace FirmwareBurner.Burning
{
    /// <summary>Менеджер зашивки образа</summary>
    /// <typeparam name="TImage">Тип получаемого образа</typeparam>
    public class BurningReceipt<TImage> : IBurningReceipt where TImage : IImage
    {
        private readonly IBurningToolFacade<TImage> _burningToolFacade;
        private readonly IImageFormatter<TImage> _formatter;

        public BurningReceipt(string Name, IImageFormatter<TImage> Formatter, IBurningToolFacade<TImage> BurningToolFacade)
        {
            _formatter = Formatter;
            _burningToolFacade = BurningToolFacade;
            this.Name = Name;
        }

        /// <summary>Название рецепта</summary>
        public string Name { get; private set; }

        /// <summary>Прошивает указанный проект</summary>
        /// <param name="Project">Проект для прожигания</param>
        public void Burn(FirmwareProject Project)
        {
            TImage image = _formatter.GetImage(Project);
            _burningToolFacade.Burn(image);
        }
    }
}
