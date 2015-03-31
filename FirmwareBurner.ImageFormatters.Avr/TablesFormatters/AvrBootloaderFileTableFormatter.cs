using System;
using System.Collections.Generic;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Avr.TablesFormatters
{
    /// <summary>Формирователь таблицы файлов для AVR-загрузчика</summary>
    internal class AvrBootloaderFileTableFormatter : IAvrFileTableFormatter
    {
        /// <summary>Записывает таблицу файлов в указанный буфер</summary>
        /// <param name="DestinationBuffer">Буффер, в который будет записана таблица файлов</param>
        /// <param name="Files">Список файлов, из которого будет сформирована таблица</param>
        /// <param name="StartAddress">Адрес, по которому должен быть помещена таблица файлов</param>
        public void PlaceFiles(IBuffer DestinationBuffer, ICollection<AvrImageFile> Files, int StartAddress)
        {
            var writer = new BufferWriter(DestinationBuffer, StartAddress);
            writer.WriteByte((byte)Files.Count);
            foreach (AvrImageFile file in Files)
            {
                UInt32 address = file.Address | GetMemoryAddressOffset(file.Memory);
                writer.WriteUInt32(address);
                writer.WriteUInt32((UInt32)file.Content.Length);
                writer.WriteUInt32(file.Checksum);
            }
        }

        /// <summary>Вычисляет отступ адреса для указанного типа памяти</summary>
        /// <param name="Memory">Тип памяти, в котором будет располагаться файл</param>
        private uint GetMemoryAddressOffset(MemoryKind Memory)
        {
            uint bit = Memory == MemoryKind.Flash ? 0u : 1u;
            return bit << 24;
        }
    }
}
