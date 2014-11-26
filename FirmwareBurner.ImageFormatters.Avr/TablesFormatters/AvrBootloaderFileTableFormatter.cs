using System;
using System.Collections.Generic;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Avr.TablesFormatters
{
    /// <summary>Формирователь таблицы файлов для AVR-загрузчика</summary>
    internal class AvrBootloaderFileTableFormatter : IBinaryFileTableFormatter
    {
        /// <summary>Записывает таблицу файлов в указанный буфер</summary>
        /// <param name="DestinationBuffer">Буффер, в который будет записана таблица файлов</param>
        /// <param name="Files">Список файлов, из которого будет сформирована таблица</param>
        /// <param name="StartAddress">Адрес, по которому должен быть помещена таблица файлов</param>
        public void PlaceFiles(IBuffer DestinationBuffer, ICollection<ImageFile> Files, int StartAddress)
        {
            var writer = new BufferWriter(DestinationBuffer, StartAddress);
            writer.WriteByte((byte)Files.Count);
            foreach (ImageFile file in Files)
            {
                writer.WriteUInt32(file.Address);
                writer.WriteUInt32((UInt32)file.Content.Length);
                writer.WriteUInt32(file.Checksum);
            }
        }
    }
}
