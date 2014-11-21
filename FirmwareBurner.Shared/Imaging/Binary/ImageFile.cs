using System;

namespace FirmwareBurner.Implementations.Avr
{
    public class ImageFile
    {
        /// <summary>Инициализирует новый экземпляр класса <see cref="T:System.Object" />.</summary>
        public ImageFile(int Address, byte[] Content, ushort Checksum)
        {
            this.Address = Address;
            this.Content = Content;
            this.Checksum = Checksum;
        }

        public int Address { get; private set; }
        public Byte[] Content { get; private set; }
        public UInt16 Checksum { get; private set; }
    }
}