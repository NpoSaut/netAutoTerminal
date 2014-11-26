using FirmwareBurner.Annotations;

namespace FirmwareBurner.ImageFormatters.Avr
{
    public class AvrBootloaderInformation
    {
        /// <summary>Инициализирует новый экземпляр класса <see cref="T:System.Object" />.</summary>
        public AvrBootloaderInformation(AvrFuses RequiredFuses, PlacementsInformation Placements)
        {
            this.Placements = Placements;
            this.RequiredFuses = RequiredFuses;
        }

        /// <summary>Размещение разделов в бинарной прошивке</summary>
        public PlacementsInformation Placements { get; private set; }

        /// <summary>Необходимые значения FUSE-битов</summary>
        public AvrFuses RequiredFuses { get; private set; }

        /// <summary>Получает тело загрузчика</summary>
        [NotNull]
        public byte[] GetBootloaderBody()
        {
            // TODO: Return bootloader body!!!
            return new byte[10];
        }
    }
}
