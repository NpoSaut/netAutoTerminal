using System;
using FirmwareBurner.Annotations;

namespace FirmwareBurner.Imaging
{
    /// <summary>Фабрика составителей образов</summary>
    /// <remarks>Создаёт инструменты составления образа для указанного </remarks>
    public interface IImageFormatterFactory<out TImage> where TImage : IImage
    {
        /// <summary>Создаёт сборщик образа для указанного типа устройства</summary>
        IImageFormatter<TImage> GetFormatter();

        [NotNull]
        ImageFormatterInformation Information { get; }
    }
}
