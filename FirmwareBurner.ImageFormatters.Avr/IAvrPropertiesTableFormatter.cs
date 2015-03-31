using System.Collections.Generic;
using FirmwareBurner.Annotations;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Avr
{
    /// <summary>Инструмент, записывающий в выходной буффер таблицу свойств в нужном формате</summary>
    public interface IAvrPropertiesTableFormatter
    {
        /// <summary>Записывает таблицу свойств в указанный буффер</summary>
        /// <param name="DestinationBuffer">Буффер, в который будет записана таблица свойств</param>
        /// <param name="Properties">Список формирования таблицы</param>
        /// <param name="StartAddress">Адрес, по которому должен быть помещена таблица свойств</param>
        void WriteProperties([NotNull] IBuffer DestinationBuffer, [NotNull] List<ParamRecord> Properties, int StartAddress);
    }
}
