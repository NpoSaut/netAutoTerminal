﻿using System.IO;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Avr
{
    /// <summary>Образ прошивки для AVR-устройства</summary>
    public class AvrImage : IBinaryImage
    {
        /// <summary>Инициализирует новый экземпляр класса <see cref="T:System.Object" />.</summary>
        public AvrImage(AvrFuses Fuses, Stream FlashBuffer, Stream EepromBuffer)
        {
            this.Fuses = Fuses;
            this.FlashBuffer = FlashBuffer;
            this.EepromBuffer = EepromBuffer;
        }

        /// <summary>Необходимые FUSE-биты</summary>
        public AvrFuses Fuses { get; private set; }

        /// <summary>Содержимое Flash-памяти</summary>
        public Stream FlashBuffer { get; private set; }

        /// <summary>Содержимое EEPROM</summary>
        public Stream EepromBuffer { get; private set; }
    }
}