using FirmwareBurner.Imaging.Binary;

namespace FirmwareBurner.ImageFormatters.Avr
{
    /// <summary>Образ файла в памяти AVR-устройства</summary>
    public class AvrImageFile : ImageFile
    {
        /// <summary>Создаёт образ файла в памяти AVR-устройства</summary>
        /// <param name="Memory">Тип памяти</param>
        /// <param name="Address">Адрес начала файла</param>
        /// <param name="Content">Содержимое файла</param>
        /// <param name="Checksum">Контрольная сумма</param>
        public AvrImageFile(MemoryKind Memory, uint Address, byte[] Content, ushort Checksum) : base(Address, Content, Checksum) { this.Memory = Memory; }

        public MemoryKind Memory { get; private set; }
    }
}
