using System;
using System.IO;
using System.Reflection;
using FirmwareBurner.Annotations;

namespace FirmwareBurner.ImageFormatters.Avr
{
    public class AvrBootloaderInformation
    {
        private readonly string _bootloaderBodyResourceName;

        /// <summary>Инициализирует новый экземпляр класса <see cref="T:System.Object" />.</summary>
        public AvrBootloaderInformation(AvrFuses RequiredFuses, PlacementsInformation Placements, string BootloaderBodyResourceName)
        {
            _bootloaderBodyResourceName = BootloaderBodyResourceName;
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
            Stream bodyResourceStream = Assembly.GetAssembly(typeof (StaticAvrBootloadersCatalog)).GetManifestResourceStream(_bootloaderBodyResourceName);
            if (bodyResourceStream == null)
                throw new ApplicationException("Не удалось найти ресурс с телом загрузчика");
            using (var body = new MemoryStream())
            {
                bodyResourceStream.CopyTo(body);
                return body.ToArray();
            }
        }
    }
}
