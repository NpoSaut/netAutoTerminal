using System.Collections.Generic;
using System.Linq;
using AsyncOperations.Progress;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.ImageFormatters.Binary.DataSections.Special;
using FirmwareBurner.ImageFormatters.Binary.FileParsers;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.Project;

namespace FirmwareBurner.ImageFormatters.Cortex
{
    public class CortexEmptyImageFormatter : BinaryFormatterBase<CortexImage, CortexMemoryKind>
    {
        private static readonly IDictionary<string, CortexMemoryKind> _memoryKinds =
            new Dictionary<string, CortexMemoryKind>
            {
                { "f", CortexMemoryKind.Flash }
            };

        public CortexEmptyImageFormatter(IProgressControllerFactory ProgressControllerFactory, IBufferFactory BufferFactory)
            : base(ProgressControllerFactory, BufferFactory, new DoubleLayerFileParser<CortexMemoryKind>(_memoryKinds)) { }

        /// <summary>Перечисляет все секции данных в образе</summary>
        /// <param name="Project">Проект образа</param>
        /// <param name="ModuleProject"></param>
        /// <param name="FirmwareFiles"></param>
        protected override IEnumerable<IDataSection<CortexMemoryKind>> GetDataSections(FirmwareProject Project, ModuleProject ModuleProject,
                                                                                       IList<BinaryImageFile<CortexMemoryKind>> FirmwareFiles)
        {
            return FirmwareFiles.Select(file => new ImageFileDataSection<CortexMemoryKind>(file));
        }

        /// <summary>Перечислите типы памяти, присутствующие в образе</summary>
        protected override IEnumerable<CortexMemoryKind> EnumerateMemoryKinds()
        {
            return _memoryKinds.Values;
        }

        /// <summary>Создайте экземпляр образа прошивки</summary>
        /// <param name="Buffers">Буферы для всех перечисленных в
        ///     <see cref="BootloaderBinaryFormatterBase{TImage,TMemoryKind,TBootloaderInformationKind}.EnumerateMemoryKinds" />
        ///     типов памяти</param>
        protected override CortexImage CreateImage(IDictionary<CortexMemoryKind, IBuffer> Buffers)
        {
            return new CortexImage(Buffers[CortexMemoryKind.Flash]);
        }
    }
}
