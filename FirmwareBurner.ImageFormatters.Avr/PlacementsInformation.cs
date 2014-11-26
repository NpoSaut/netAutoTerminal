namespace FirmwareBurner.ImageFormatters.Avr
{
    /// <summary>Информация о размещении разделов в бинарной прошивке</summary>
    public class PlacementsInformation
    {
        public PlacementsInformation(int BootloaderPlacement, int FilesystemIntexPlacement, int PropertiesTablePlacement)
        {
            this.BootloaderPlacement = BootloaderPlacement;
            this.FilesystemIntexPlacement = FilesystemIntexPlacement;
            this.PropertiesTablePlacement = PropertiesTablePlacement;
        }

        /// <summary>Адрес размещения загрузчика</summary>
        public int BootloaderPlacement { get; private set; }

        /// <summary>Адрес размещения таблицы файловой системы</summary>
        public int FilesystemIntexPlacement { get; private set; }

        /// <summary>Адрес размещения таблицы свойств</summary>
        public int PropertiesTablePlacement { get; private set; }
    }
}
