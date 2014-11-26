using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.Project;

namespace FirmwareBurner.ImageFormatters.Avr
{
    /// <summary>Составитель образов загрузчика для AVR-устройств</summary>
    public class AvrImageFormatter : IImageFormatter<AvrImage>
    {
        private readonly AvrBootloaderInformation _bootloaderInformation;
        private readonly IBufferFactory _bufferFactory;

        private readonly IBinaryFileTableFormatter _fileTableFormatter;
        private readonly IBinaryPropertiesTableFormatter _propertiesTableFormatter;

        private readonly IPropertiesTableGenerator _propertiesTableGenerator;

        public AvrImageFormatter(AvrBootloaderInformation BootloaderInformation,
                                 IPropertiesTableGenerator PropertiesTableGenerator, IBufferFactory BufferFactory, IBinaryFileTableFormatter FileTableFormatter,
                                 IBinaryPropertiesTableFormatter PropertiesTableFormatter)
        {
            _bootloaderInformation = BootloaderInformation;
            _propertiesTableGenerator = PropertiesTableGenerator;
            _bufferFactory = BufferFactory;
            _fileTableFormatter = FileTableFormatter;
            _propertiesTableFormatter = PropertiesTableFormatter;
        }

        /// <summary>Генерирует образ для указанного проекта прошивки</summary>
        /// <param name="Project">Проект прошивки</param>
        /// <returns>Образ прошивки</returns>
        public AvrImage GetImage(FirmwareProject Project)
        {
            // Подготовка буферов
            IBuffer flashBuffer = _bufferFactory.CreateBuffer();
            IBuffer eepromBuffer = _bufferFactory.CreateBuffer();

            var buffers = new Dictionary<string, IBuffer>
                          {
                              { "f", flashBuffer },
                              { "e", eepromBuffer }
                          };

            // Подготовка списка файлов
            ModuleProject moduleProject = Project.Modules.Single();
            Dictionary<string, ImageFile> firmwareFiles = moduleProject.FirmwareContent.Files
                                                                       .Select(f => new { nameParts = f.RelativePath.Split('/'), f.Content })
                                                                       .ToDictionary(fx => fx.nameParts[0],
                                                                                     fx => new ImageFile(UInt32.Parse(fx.nameParts[1], NumberStyles.HexNumber),
                                                                                                         fx.Content,
                                                                                                         FudpCrc.CalcCrc(fx.Content)));

            // Запись таблицы файлов
            _fileTableFormatter.PlaceFiles(flashBuffer, firmwareFiles.Values, _bootloaderInformation.Placements.FilesystemIntexPlacement);

            // Запись содержимого файлов
            foreach (var file in firmwareFiles)
                buffers[file.Key].Write((int)file.Value.Address, file.Value.Content);

            // Запись свойств
            var overallProperties = new List<ParamRecord>();
            overallProperties.AddRange(_propertiesTableGenerator.GetDeviceProperties(Project.Target));
            overallProperties.AddRange(_propertiesTableGenerator.GetModuleProperties(moduleProject.Information, moduleProject.FirmwareInformation));
            _propertiesTableFormatter.WriteProperties(flashBuffer, overallProperties, _bootloaderInformation.Placements.PropertiesTablePlacement);

            // Запись тела загрузчика
            flashBuffer.Write(_bootloaderInformation.Placements.BootloaderPlacement,
                              _bootloaderInformation.GetBootloaderBody());

            return new AvrImage(
                _bootloaderInformation.RequiredFuses,
                flashBuffer,
                eepromBuffer);
        }
    }
}
