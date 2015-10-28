using System.Collections.Generic;
using System.Linq;
using AsyncOperations.Progress;
using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.ImageFormatters.Binary.DataSections.Special;
using FirmwareBurner.ImageFormatters.Binary.FileParsers;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.Project;

namespace FirmwareBurner.ImageFormatters.Binary
{
    public abstract class BootloaderBinaryFormatterBase<TImage, TMemoryKind, TBootloaderInformationKind> : BinaryFormatterBase<TImage, TMemoryKind>
        where TBootloaderInformationKind : BinaryBootloaderInformation<TMemoryKind>
        where TImage : IImage
    {
        protected readonly TBootloaderInformationKind BootloaderInformation;

        protected BootloaderBinaryFormatterBase(IProgressControllerFactory ProgressControllerFactory, IBufferFactory BufferFactory,
                                                TBootloaderInformationKind BootloaderInformation, IFileParser<TMemoryKind> FileParser)
            : base(ProgressControllerFactory, BufferFactory, FileParser)
        {
            this.BootloaderInformation = BootloaderInformation;
        }

        /// <summary>Перечисляет все секции данных в образе</summary>
        /// <param name="Project">Проект образа</param>
        /// <param name="ModuleProject"></param>
        /// <param name="FirmwareFiles"></param>
        protected override IEnumerable<IDataSection<TMemoryKind>> GetDataSections(FirmwareProject Project, ModuleProject ModuleProject, IList<BinaryImageFile<TMemoryKind>> FirmwareFiles)
        {
            foreach (var customDataSection in GetCustomDataSections(Project, ModuleProject, FirmwareFiles))
                yield return customDataSection;

            foreach (var file in FirmwareFiles)
                yield return GetImageFileDataSection(file);

            yield return GetBootloaderBodyDataSection();
        }

        /// <summary>Создаёт секцию в образе для указанного файла</summary>
        /// <param name="File">Файл, для которого создаётся секция</param>
        private static ImageFileDataSection<TMemoryKind> GetImageFileDataSection(BinaryImageFile<TMemoryKind> File)
        {
            return new ImageFileDataSection<TMemoryKind>(File);
        }

        /// <summary>Создаёт секцию с телом загрузчика</summary>
        protected virtual BootloaderBodyDataSection<TMemoryKind> GetBootloaderBodyDataSection()
        {
            return new BootloaderBodyDataSection<TMemoryKind>(BootloaderInformation);
        }

        /// <summary>Перечислите уникальные секции с данными для вашего загрузчика (таблицы свойств, файлов и другое)</summary>
        /// <param name="Project">Проект образа</param>
        /// <param name="ModuleProject">Программный модуль, для которого формируются секции</param>
        /// <param name="Files">Список файлов образа</param>
        protected abstract IEnumerable<IDataSection<TMemoryKind>> GetCustomDataSections(FirmwareProject Project, ModuleProject ModuleProject,
                                                                                        ICollection<BinaryImageFile<TMemoryKind>> Files);
    }
}
