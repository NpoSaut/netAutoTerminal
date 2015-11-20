using System.Collections.Generic;
using AsyncOperations.Progress;
using FirmwareBurner.ImageFormatters.Avr.Sections;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.ImageFormatters.Binary.FileParsers;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.Imaging.PropertiesProviders;
using FirmwareBurner.Project;

namespace FirmwareBurner.ImageFormatters.Avr
{
    public class AvrImageFormatter : BootloaderBinaryFormatterBase<AvrImage, AvrMemoryKind, AvrBootloaderInformation>
    {
        private readonly IChecksumProvider _checksumProvider;
        private readonly IStringEncoder _stringEncoder;

        public AvrImageFormatter(IProgressControllerFactory ProgressControllerFactory, IBufferFactory BufferFactory,
                                 AvrBootloaderInformation BootloaderInformation, IFileParser<AvrMemoryKind> FileParser, IChecksumProvider ChecksumProvider,
                                 IStringEncoder StringEncoder)
            : base(ProgressControllerFactory, BufferFactory, BootloaderInformation, FileParser)
        {
            _checksumProvider = ChecksumProvider;
            _stringEncoder = StringEncoder;
        }

        /// <summary>Перечислите типы памяти, присутствующие в образе</summary>
        protected override IEnumerable<AvrMemoryKind> EnumerateMemoryKinds()
        {
            return new[] { AvrMemoryKind.Flash, AvrMemoryKind.Eeprom };
        }

        protected override AvrImage CreateImage(IDictionary<AvrMemoryKind, IBuffer> Buffers)
        {
            return new AvrImage(BootloaderInformation.RequiredFuses, Buffers[AvrMemoryKind.Flash], Buffers[AvrMemoryKind.Eeprom]);
        }

        /// <summary>Перечислите уникальные секции с данными для вашего загрузчика (таблицы свойств, файлов и другое)</summary>
        /// <param name="Project">Проект образа</param>
        /// <param name="ModuleProject">Программный модуль, для которого формируются секции</param>
        /// <param name="Files">Список файлов образа</param>
        protected override IEnumerable<IDataSection<AvrMemoryKind>> GetCustomDataSections(FirmwareProject Project, ModuleProject ModuleProject,
                                                                                          ICollection<BinaryImageFile<AvrMemoryKind>> Files)
        {
            yield return new DataSection<AvrMemoryKind>(
                BootloaderInformation.PropertiesTablePlacement,
                new FilesTableSectionContent(_checksumProvider, Files));

            yield return new DataSection<AvrMemoryKind>(
                BootloaderInformation.PropertiesTablePlacement,
                new PropertiesTableSectionContent(
                    new CompositePropertiesProvider(
                        new DevicePropertiesProvider(Project.Target),
                        new ModulePropertiesProvider(ModuleProject.Information),
                        new SoftwarePropertiesProvider(ModuleProject, _stringEncoder, _checksumProvider))));
        }
    }
}
