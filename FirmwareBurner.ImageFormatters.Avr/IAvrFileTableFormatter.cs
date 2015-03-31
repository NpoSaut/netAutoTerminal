using System.Collections.Generic;
using FirmwareBurner.Annotations;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Avr
{
    /// <summary>Инструмент, записывающий в выходной буффер таблицу свойств в нужном формате</summary>
    public interface IAvrFileTableFormatter
    {
        /// <summary>Записывает таблицу файлов в указанный буфер</summary>
        /// <param name="DestinationBuffer">Буффер, в который будет записана таблица файлов</param>
        /// <param name="Files">Список файлов, из которого будет сформирована таблица</param>
        /// <param name="StartAddress">Адрес, по которому должен быть помещена таблица файлов</param>
        void PlaceFiles([NotNull] IBuffer DestinationBuffer, [NotNull] ICollection<AvrImageFile> Files, int StartAddress);
    }
}
