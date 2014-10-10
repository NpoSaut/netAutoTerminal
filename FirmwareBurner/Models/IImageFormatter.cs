using FirmwareBurner.Models.Images;
using FirmwareBurner.Models.Project;

namespace FirmwareBurner.Models
{
    /// <summary>Инструмент по созданию образа из проекта</summary>
    /// <typeparam name="TImage">Тип получаемого образа</typeparam>
    public interface IImageFormatter<out TImage> where TImage : IImage
    {
        /// <summary>Генерирует образ для указанного проекта прошивки</summary>
        /// <param name="Project">Проект прошивки</param>
        /// <returns>Образ прошивки</returns>
        TImage GetImage(FirmwareProject Project);
    }
}
