using System.Collections.Generic;
using AsyncOperations.Progress;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.ImageFormatters.Binary.FileParsers;
using FirmwareBurner.ImageFormatters.Cortex.Catalog;
using FirmwareBurner.ImageFormatters.Cortex.Sections;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.Imaging.PropertiesProviders;
using FirmwareBurner.Project;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    public class CortexImageFormatter : BootloaderBinaryFormatterBase<CortexImage, CortexMemoryKind, CortexBootloaderInformation>
    {
        private static readonly IDictionary<string, CortexMemoryKind> _memoryKinds =
            new Dictionary<string, CortexMemoryKind>
            {
                { "f", CortexMemoryKind.Flash }
            };

        private readonly IBootloaderConfigurationCatalog _bootloaderConfigurationCatalog;

        private readonly IChecksumProvider _checksumProvider;
        private readonly IStringEncoder _stringEncoder;

        public CortexImageFormatter(IProgressControllerFactory ProgressControllerFactory, IBufferFactory BufferFactory,
                                    IChecksumProvider ChecksumProvider, IStringEncoder StringEncoder,
                                    IBootloaderConfigurationCatalog BootloaderConfigurationCatalog, CortexBootloaderInformation BootloaderInformation)
            : base(ProgressControllerFactory, BufferFactory, BootloaderInformation, new DoubleLayerFileParser<CortexMemoryKind>(_memoryKinds))
        {
            _checksumProvider = ChecksumProvider;
            _stringEncoder = StringEncoder;
            _bootloaderConfigurationCatalog = BootloaderConfigurationCatalog;
        }

        /// <summary>Перечислите типы памяти, присутствующие в образе</summary>
        protected override IEnumerable<CortexMemoryKind> EnumerateMemoryKinds() { yield return CortexMemoryKind.Flash; }

        protected override IEnumerable<IDataSection<CortexMemoryKind>> GetCustomDataSections(FirmwareProject Project, ModuleProject ModuleProject,
                                                                                             ICollection<BinaryImageFile<CortexMemoryKind>> Files)
        {
            yield return new DataSection<CortexMemoryKind>(
                BootloaderInformation.StaticPropertiesPlacement,
                new CortexPropertiesSectionContent(
                    new CompositePropertiesProvider(
                        new DevicePropertiesProvider(Project.Target),
                        new ModulePropertiesProvider(ModuleProject.Information),
                        new CortexBootloaderPropertiesProvider(Project.Target, _bootloaderConfigurationCatalog))));

            yield return new DataSection<CortexMemoryKind>(
                BootloaderInformation.DynamicPropertiesPlacement,
                new ChecksumSectionContentDecorator<CortexMemoryKind>(
                    RelativePosition.Enclosing,
                    new FudpCrcChecksumProvider(),
                    new ConsecutiveSectionContent<CortexMemoryKind>(
                        new StaticSectionContent<CortexMemoryKind>(0x56, 0x65),
                        new CortexPropertiesSectionContent(
                            new CompositePropertiesProvider(
                                new SoftwarePropertiesProvider(ModuleProject, _stringEncoder, _checksumProvider))),
                        new CortexFileTableSectionContent(Files, _memoryKinds, _checksumProvider))));
        }

        protected override CortexImage CreateImage(IDictionary<CortexMemoryKind, IBuffer> Buffers) { return new CortexImage(Buffers[CortexMemoryKind.Flash]); }
    }
}
