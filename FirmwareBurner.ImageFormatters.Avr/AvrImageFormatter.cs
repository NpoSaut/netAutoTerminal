using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FirmwareBurner.Models;
using FirmwareBurner.Models.Project;
using FirmwarePacking;

namespace FirmwareBurner.Implementations.Avr
{
    public class AvrImageFormatter : IImageFormatter<AvrImage>
    {
        private readonly IAvrBootloaderInformation _bootloaderInformation;

        private readonly Dictionary<String, String> _bufferPseudonames =
            new Dictionary<string, string>
            {
                { "f", "Flash" },
                { "e", "EEPROM" }
            };

        private readonly IBinaryImageBuilder<AvrImage> _imageBuilder;
        private readonly IPropertiesTableGenerator _propertiesTableGenerator;

        public AvrImageFormatter(IBinaryImageBuilder<AvrImage> ImageBuilder, IAvrBootloaderInformation BootloaderInformation,
                                 IPropertiesTableGenerator PropertiesTableGenerator)
        {
            _imageBuilder = ImageBuilder;
            _bootloaderInformation = BootloaderInformation;
            _propertiesTableGenerator = PropertiesTableGenerator;
        }

        /// <summary>Генерирует образ для указанного проекта прошивки</summary>
        /// <param name="Project">Проект прошивки</param>
        /// <returns>Образ прошивки</returns>
        public AvrImage GetImage(FirmwareProject Project)
        {
            var flashStream = new MemoryStream();
            var eepromStream = new MemoryStream();

            ModuleProject moduleProject = Project.Modules.Single();

            foreach (FirmwareFile file in moduleProject.FirmwareContent.Files)
            {
                string[] nameParts = file.RelativePath.Split('/');
                string bufferName = _bufferPseudonames[nameParts[0]];
                int address = int.Parse(nameParts[1]);
                var imageFile = new ImageFile(address, file.Content, FudpCrc.CalcCrc(file.Content));

                Stream destinationStream = null;
                switch (bufferName)
                {
                    case "f":
                        destinationStream = flashStream;
                        break;
                    case "e":
                        destinationStream = eepromStream;
                        break;
                }

                if (destinationStream != null)
                    _bootloaderInformation.FilesystemFormatter.AddFile(destinationStream, imageFile);
            }

            foreach (var property in _propertiesTableGenerator.Generate(Project.Target, moduleProject.Information, moduleProject.FirmwareInformation))
                _bootloaderInformation.PropertiesTableFormatter.AddPropertyValue(flashStream, property.Key, property.Value);

            byte[] bootloaderBody = _bootloaderInformation.GetBootloaderBody();
            flashStream.Seek(_bootloaderInformation.BootloaderPlacementAddress, SeekOrigin.Begin);
            flashStream.Write(bootloaderBody, 0, bootloaderBody.Length);

            return new AvrImage(
                _bootloaderInformation.RequiredFuses,
                flashStream,
                eepromStream);
        }
    }
}
