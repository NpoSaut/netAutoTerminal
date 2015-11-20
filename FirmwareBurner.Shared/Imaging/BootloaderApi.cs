namespace FirmwareBurner.Imaging
{
    public class BootloaderApi
    {
        /// <summary>Описание API загрузчика</summary>
        /// <param name="BootloaderId">Идентификатор загрузчика</param>
        /// <param name="BootloaderVersion">Версия загрузчика</param>
        /// <param name="BootloaderCompatibleVersion">Последняя совместимая версия загрузчика</param>
        public BootloaderApi(int BootloaderId, int BootloaderVersion, int BootloaderCompatibleVersion)
        {
            this.BootloaderId = BootloaderId;
            this.BootloaderVersion = BootloaderVersion;
            this.BootloaderCompatibleVersion = BootloaderCompatibleVersion;
        }

        /// <summary>Идентификатор загрузчика</summary>
        public int BootloaderId { get; private set; }

        /// <summary>Версия загрузчика</summary>
        public int BootloaderVersion { get; private set; }

        /// <summary>Последняя совместимая версия загрузчика</summary>
        public int BootloaderCompatibleVersion { get; private set; }

        /// <summary>Без загрузчика</summary>
        public static BootloaderApi Empty
        {
            get { return new BootloaderApi(0, int.MaxValue, 0); }
        }
    }
}
