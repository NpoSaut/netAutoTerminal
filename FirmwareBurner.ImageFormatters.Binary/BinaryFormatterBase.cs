using System.Collections.Generic;
using System.Linq;
using AsyncOperations.Progress;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.ImageFormatters.Binary.DataSections.Special;
using FirmwareBurner.ImageFormatters.Binary.FileParsers;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.Project;

namespace FirmwareBurner.ImageFormatters.Binary
{
    public abstract class BinaryFormatterBase<TImage, TMemoryKind, TBootloaderInformationKind> : IImageFormatter<TImage>
        where TBootloaderInformationKind : BinaryBootloaderInformation<TMemoryKind>
        where TImage : IImage
    {
        protected readonly TBootloaderInformationKind BootloaderInformation;
        private readonly IBufferFactory _bufferFactory;
        private readonly IFileParser<TMemoryKind> _fileParser;
        private readonly IProgressControllerFactory _progressControllerFactory;

        public BinaryFormatterBase(IProgressControllerFactory ProgressControllerFactory, IBufferFactory BufferFactory,
                                   TBootloaderInformationKind BootloaderInformation, IFileParser<TMemoryKind> FileParser)
        {
            _progressControllerFactory = ProgressControllerFactory;
            _bufferFactory = BufferFactory;
            this.BootloaderInformation = BootloaderInformation;
            _fileParser = FileParser;
        }

        /// <summary>Генерирует образ для указанного проекта прошивки</summary>
        /// <param name="Project">Проект образа</param>
        /// <param name="ProgressToken">Токен прогресса выполнения операции</param>
        /// <returns>Образ прошивки</returns>
        public TImage GetImage(FirmwareProject Project, IProgressToken ProgressToken)
        {
            using (_progressControllerFactory.CreateController(ProgressToken))
            {
                // Подготовка буферов
                Dictionary<TMemoryKind, IBuffer> buffers = EnumerateMemoryKinds().ToDictionary(mk => mk, mk => _bufferFactory.CreateBuffer());

                // Получаем список секций
                IEnumerable<IDataSection<TMemoryKind>> dataSections = GetDataSections(Project);

                // Записываем секции в соответствующие буферы
                foreach (var dataSection in dataSections)
                    dataSection.WriteTo(buffers[dataSection.Placement.MemoryKind]);

                // Создание образа из буферов
                return CreateImage(buffers);
            }
        }

        /// <summary>Перечисляет все секции данных в образе</summary>
        /// <param name="Project">Проект образа</param>
        protected virtual IEnumerable<IDataSection<TMemoryKind>> GetDataSections(FirmwareProject Project)
        {
            ModuleProject moduleProject = Project.Modules.Single();
            IList<BinaryImageFile<TMemoryKind>> firmwareFiles = moduleProject.FirmwareContent.Files.Select(_fileParser.GetImageFile).ToList();

            foreach (var customDataSection in GetCustomDataSections(Project, moduleProject, firmwareFiles))
                yield return customDataSection;

            foreach (var file in firmwareFiles)
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

        /// <summary>Перечислите типы памяти, присутствующие в образе</summary>
        protected abstract IEnumerable<TMemoryKind> EnumerateMemoryKinds();

        /// <summary>Создайте экземпляр образа прошивки</summary>
        /// <param name="Buffers">Буферы для всех перечисленных в <see cref="EnumerateMemoryKinds" /> типов памяти</param>
        protected abstract TImage CreateImage(IDictionary<TMemoryKind, IBuffer> Buffers);

        /// <summary>Перечислите уникальные секции с данными для вашего загрузчика (таблицы свойств, файлов и другое)</summary>
        /// <param name="Project">Проект образа</param>
        /// <param name="ModuleProject">Программный модуль, для которого формируются секции</param>
        /// <param name="Files">Список файлов образа</param>
        protected abstract IEnumerable<IDataSection<TMemoryKind>> GetCustomDataSections(FirmwareProject Project, ModuleProject ModuleProject,
                                                                                        ICollection<BinaryImageFile<TMemoryKind>> Files);
    }
}
