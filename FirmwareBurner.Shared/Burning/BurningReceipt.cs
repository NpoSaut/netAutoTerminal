using System;
using FirmwareBurner.Burning.Exceptions;
using FirmwareBurner.Imaging;
using FirmwareBurner.Progress;
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
        /// <param name="Progress">Токен для отчёта о процессе прошивки</param>
        public void Burn(FirmwareProject Project, IProgressToken Progress)
        {
            var imageProgress = new SubprocessProgressToken(0.1);
            var burnProgress = new SubprocessProgressToken();

            using (new CompositeProgressManager(Progress, imageProgress, burnProgress))
            {
                try
                {
                    TImage image = _formatter.GetImage(Project, imageProgress);
                    _burningToolFacade.Burn(image, burnProgress);
                }
                catch (Exception e)
                {
                    throw new BurningException("Во время прошивки возникло исключение", e);
                }
            }
        }
    }
}
