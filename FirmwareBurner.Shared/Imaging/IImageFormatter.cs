using System;
using FirmwareBurner.Project;

namespace FirmwareBurner.Imaging
{
    /// <summary>Инструмент по созданию образа из проекта</summary>
    /// <typeparam name="TImage">Тип получаемого образа</typeparam>
    public interface IImageFormatter<out TImage> where TImage : IImage
    {
        /// <summary>Генерирует образ для указанного проекта прошивки</summary>
        /// <param name="Project">Проект образа</param>
        /// <param name="DeviceName">Название устройства, под которое будет собран образ</param>
        /// <returns>Образ прошивки</returns>
        TImage GetImage(FirmwareProject Project, String DeviceName);
    }
}
