using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AsyncOperations.Progress;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.Project;
using FirmwarePacking;

namespace FirmwareBurner.ImageFormatters.Avr
{
    /// <summary>Составитель образов загрузчика для AVR-устройств</summary>
    public class AvrImageFormatter : IImageFormatter<AvrImage>
    {
        private readonly AvrBootloaderInformation _bootloaderInformation;
        private readonly IBufferFactory _bufferFactory;

        private readonly IAvrFileTableFormatter _fileTableFormatter;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IAvrPropertiesTableFormatter _propertiesTableFormatter;

        private readonly IPropertiesTableGenerator _propertiesTableGenerator;

        public AvrImageFormatter(AvrBootloaderInformation BootloaderInformation,
                                 IPropertiesTableGenerator PropertiesTableGenerator, IBufferFactory BufferFactory, IAvrFileTableFormatter FileTableFormatter,
                                 IAvrPropertiesTableFormatter PropertiesTableFormatter, IProgressControllerFactory ProgressControllerFactory)
        {
            _bootloaderInformation = BootloaderInformation;
            _propertiesTableGenerator = PropertiesTableGenerator;
            _bufferFactory = BufferFactory;
            _fileTableFormatter = FileTableFormatter;
            _propertiesTableFormatter = PropertiesTableFormatter;
            _progressControllerFactory = ProgressControllerFactory;
        }

        /// <summary>Генерирует образ для указанного проекта прошивки</summary>
        /// <param name="Project">Проект прошивки</param>
        /// <param name="ProgressToken">Токен прогресса выполнения операции</param>
        /// <returns>Образ прошивки</returns>
        public AvrImage GetImage(FirmwareProject Project, IProgressToken ProgressToken)
        {
            using (_progressControllerFactory.CreateController(ProgressToken))
            {
                // Подготовка буферов
                IBuffer flashBuffer = _bufferFactory.CreateBuffer();
                IBuffer eepromBuffer = _bufferFactory.CreateBuffer();

                var buffers = new Dictionary<MemoryKind, IBuffer>
                              {
                                  { MemoryKind.Flash, flashBuffer },
                                  { MemoryKind.Eeprom, eepromBuffer }
                              };

                // Подготовка списка файлов
                ModuleProject moduleProject = Project.Modules.Single();
                List<AvrImageFile> firmwareFiles = moduleProject.FirmwareContent.Files.Select(ParsePackageFile).ToList();

                // Запись таблицы файлов
                _fileTableFormatter.PlaceFiles(flashBuffer, firmwareFiles, _bootloaderInformation.Placements.FilesystemIntexPlacement);

                // Запись содержимого файлов
                foreach (AvrImageFile file in firmwareFiles)
                    buffers[file.Memory].Write((int)file.Address, file.Content);

                // Запись свойств
                var overallProperties = new List<ParamRecord>();
                overallProperties.AddRange(_propertiesTableGenerator.GetDeviceProperties(Project, moduleProject.Information.ModuleId));
                overallProperties.AddRange(_propertiesTableGenerator.GetModuleProperties(moduleProject));
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

        private AvrImageFile ParsePackageFile(FirmwareFile File)
        {
            string[] nameParts = File.RelativePath.Split('/');
            if (nameParts.Length != 2) throw new AvrImageFormatterParseFilenameException(File);

            MemoryKind memoryKind;
            switch (nameParts[0].ToLower())
            {
                case "f":
                    memoryKind = MemoryKind.Flash;
                    break;
                case "e":
                    memoryKind = MemoryKind.Eeprom;
                    break;
                default:
                    throw new AvrImageFormatterParseFilenameException(File, "Не удаётся определить тип памяти");
            }

            uint address;
            if (!UInt32.TryParse(nameParts[1], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out address))
                throw new AvrImageFormatterParseFilenameException(File, "Не удаётся определить тип памяти");

            return new AvrImageFile(memoryKind, address, File.Content, FudpCrc.CalcCrc(File.Content));
        }
    }
}
