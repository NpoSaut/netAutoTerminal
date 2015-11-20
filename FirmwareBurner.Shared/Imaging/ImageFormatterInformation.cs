namespace FirmwareBurner.Imaging
{
    /// <summary>Информация о составителе образа</summary>
    public class ImageFormatterInformation
    {
        public ImageFormatterInformation(string Name, BootloaderApi BootloaderApi)
        {
            this.Name = Name;
            this.BootloaderApi = BootloaderApi;
        }

        /// <summary>Название составителя образа</summary>
        public string Name { get; private set; }

        /// <summary>Описание API загрузчика</summary>
        public BootloaderApi BootloaderApi { get; private set; }
    }
}
