using System.Collections.Generic;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.Imaging.Binary
{
    /// <summary>Инструмент, записывающий в выходной буффер таблицу свойств в нужном формате</summary>
    public interface IBinaryFileTableFormatter
    {
        /// <summary>Записывает таблицу файлов в указанный буфер</summary>
        /// <param name="DestinationBuffer">Буффер, в который будет записана таблица файлов</param>
        /// <param name="Files">Список файлов, из которого будет сформирована таблица</param>
        /// <param name="StartAddress">Адрес, по которому должен быть помещена таблица файлов</param>
        void PlaceFiles(IBuffer DestinationBuffer, ICollection<ImageFile> Files, int StartAddress);
    }
}
