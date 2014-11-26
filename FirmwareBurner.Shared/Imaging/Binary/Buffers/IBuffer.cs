﻿using System.IO;
using FirmwareBurner.Annotations;

namespace FirmwareBurner.Imaging.Binary.Buffers
{
    /// <summary>Буфер бинарных данных</summary>
    public interface IBuffer
    {
        /// <summary>Записывает массив байт в указанное место буфера</summary>
        /// <param name="Position">Адрес, по которому следует разместить первый байт</param>
        /// <param name="Bytes">Данные для записи</param>
        void Write(int Position, [NotNull] params byte[] Bytes);

        /// <summary>Записывает данные из буфера в указанный поток</summary>
        /// <param name="DestinationStream">Поток, в который будут записаны данные из буфера</param>
        void CopyTo([NotNull] Stream DestinationStream);
    }
}