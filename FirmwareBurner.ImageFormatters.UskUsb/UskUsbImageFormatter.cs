using System.Collections.Generic;
using AsyncOperations.Progress;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.ImageFormatters.Binary.FileParsers;
using FirmwareBurner.ImageFormatters.Cortex;
using FirmwareBurner.ImageFormatters.UskUsb.Sections;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.Imaging.PropertiesProviders;
using FirmwareBurner.Project;

namespace FirmwareBurner.ImageFormatters.UskUsb
{
    public class UskUsbImageFormatter : BootloaderBinaryFormatterBase<CortexImage, CortexMemoryKind, UskUsbBootloaderInformation>
    {
        private readonly IChecksumProvider _checksumProvider;
        private readonly IStringEncoder _stringEncoder;

        public UskUsbImageFormatter(IProgressControllerFactory ProgressControllerFactory, IBufferFactory BufferFactory,
                                    UskUsbBootloaderInformation BootloaderInformation, IFileParser<CortexMemoryKind> FileParser,
                                    IChecksumProvider ChecksumProvider, IStringEncoder StringEncoder)
            : base(ProgressControllerFactory, BufferFactory, BootloaderInformation, FileParser)
        {
            _checksumProvider = ChecksumProvider;
            _stringEncoder = StringEncoder;
        }

        /// <summary>Перечислите типы памяти, присутствующие в образе</summary>
        protected override IEnumerable<CortexMemoryKind> EnumerateMemoryKinds() { yield return CortexMemoryKind.Flash; }

        /// <summary>Создайте экземпляр образа прошивки</summary>
        /// <param name="Buffers">Буферы для всех перечисленных в
        ///     <see cref="BinaryFormatterBase{TImage,TMemoryKind}.EnumerateMemoryKinds" /> типов памяти</param>
        protected override CortexImage CreateImage(IDictionary<CortexMemoryKind, IBuffer> Buffers) { return new CortexImage(Buffers[CortexMemoryKind.Flash]); }

        /// <summary>Перечислите уникальные секции с данными для вашего загрузчика (таблицы свойств, файлов и другое)</summary>
        /// <param name="Project">Проект образа</param>
        /// <param name="ModuleProject">Программный модуль, для которого формируются секции</param>
        /// <param name="Files">Список файлов образа</param>
        protected override IEnumerable<IDataSection<CortexMemoryKind>> GetCustomDataSections(FirmwareProject Project, ModuleProject ModuleProject,
                                                                                             ICollection<BinaryImageFile<CortexMemoryKind>> Files)
        {
            yield return new DataSection<CortexMemoryKind>(
                BootloaderInformation.FileTablePlacement,
                new FilesTableSectionContent(_checksumProvider, Files));

            yield return new DataSection<CortexMemoryKind>(
                BootloaderInformation.PropertiesPlacement,
                new PropertiesTableContent(
                    new CompositePropertiesProvider(
                        new DevicePropertiesProvider(Project.Target),
                        new ModulePropertiesProvider(ModuleProject.Information),
                        new SoftwarePropertiesProvider(ModuleProject, _stringEncoder, _checksumProvider))));
        }
    }
}
