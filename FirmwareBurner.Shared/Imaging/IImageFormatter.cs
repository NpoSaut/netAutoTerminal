using AsyncOperations.Progress;
using FirmwareBurner.Project;

namespace FirmwareBurner.Imaging
{
    /// <summary>Сборщик образа</summary>
    /// <typeparam name="TImage">Тип получаемого образа</typeparam>
    public interface IImageFormatter<out TImage> where TImage : IImage
    {
        /// <summary>Генерирует образ для указанного проекта прошивки</summary>
        /// <param name="Project">Проект образа</param>
        /// <param name="ProgressToken">Токен прогресса выполнения операции</param>
        /// <returns>Образ прошивки</returns>
        TImage GetImage(FirmwareProject Project, IProgressToken ProgressToken);
    }
}
