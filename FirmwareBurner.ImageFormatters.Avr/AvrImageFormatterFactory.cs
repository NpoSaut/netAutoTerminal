using System;
using System.Collections.Generic;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Avr
{
    public class AvrImageFormatterFactory : IImageFormatterFactory<AvrImage>
    {
        private readonly IDictionary<String, IAvrBootloaderInformation> _bootloadersCatalog;
        private readonly IPropertiesTableGenerator _propertiesTableGenerator;

        public AvrImageFormatterFactory(IPropertiesTableGenerator PropertiesTableGenerator)
        {
            _propertiesTableGenerator = PropertiesTableGenerator;
            _bootloadersCatalog = new Dictionary<string, IAvrBootloaderInformation>();
        }

        public IImageFormatter<AvrImage> GetFormatter(string DeviceName)
        {
            return new AvrImageFormatter(_bootloadersCatalog[DeviceName], _propertiesTableGenerator);
        }
    }
}
