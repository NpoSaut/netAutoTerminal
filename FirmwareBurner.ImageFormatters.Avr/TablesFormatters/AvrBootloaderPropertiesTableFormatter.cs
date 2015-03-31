using System.Collections.Generic;
using System.IO;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Avr.TablesFormatters
{
    /// <summary>Формирователь таблицы свойств для Диминого AVR-загрузчика</summary>
    public class AvrBootloaderPropertiesTableFormatter : IAvrPropertiesTableFormatter
    {
        /// <summary>
        ///     Записывает в выходной бинарный поток <paramref name="DestinationBuffer" /> в указанные свойства в нужном
        ///     формате в позицию, в которой стоит указатель <see cref="Stream.Position" />
        /// </summary>
        /// <param name="DestinationBuffer">Выходной поток</param>
        /// <param name="Properties">Список свойств для записи</param>
        /// <param name="StartAddress">Адрес, по которому должен быть помещена таблица свойств</param>
        public void WriteProperties(IBuffer DestinationBuffer, List<ParamRecord> Properties, int StartAddress)
        {
            var writer = new BufferWriter(DestinationBuffer, StartAddress);
            writer.WriteByte((byte)Properties.Count);
            foreach (ParamRecord property in Properties)
            {
                writer.WriteByte(property.Key);
                writer.WriteInt32(property.Value);
            }
        }
    }
}
