using System;
using FirmwareBurner.Annotations;

namespace FirmwareBurner.Imaging
{
    /// <summary>Фабрика составителей образов</summary>
    /// <remarks>Создаёт инструменты составления образа для указанного </remarks>
    /// <typeparam name="TImage"></typeparam>
    public interface IImageFormatterFactory<out TImage> where TImage : IImage
    {
        /// <summary>Создаёт сборщик образа для указанного типа устройства</summary>
        /// <param name="DeviceName">Тип устройства</param>
        IImageFormatter<TImage> GetFormatter(String DeviceName);

        [NotNull]
        ImageFormatterInformation Information { get; }
    }
}
