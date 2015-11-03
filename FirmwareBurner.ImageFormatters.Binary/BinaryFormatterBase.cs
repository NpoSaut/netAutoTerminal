using System.Collections.Generic;
using System.Linq;
using AsyncOperations.Progress;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.ImageFormatters.Binary.FileParsers;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.Project;

namespace FirmwareBurner.ImageFormatters.Binary
{
    public abstract class BinaryFormatterBase<TImage, TMemoryKind> : IImageFormatter<TImage> where TImage : IImage
    {
        private readonly IBufferFactory _bufferFactory;
        private readonly IFileParser<TMemoryKind> _fileParser;
        private readonly IProgressControllerFactory _progressControllerFactory;

        protected BinaryFormatterBase(ImageFormatterInformation Information, IProgressControllerFactory ProgressControllerFactory,
                                      IBufferFactory BufferFactory, IFileParser<TMemoryKind> FileParser)
        {
            _progressControllerFactory = ProgressControllerFactory;
            _bufferFactory = BufferFactory;
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

                ModuleProject moduleProject = Project.Modules.Single();
                IList<BinaryImageFile<TMemoryKind>> firmwareFiles = moduleProject.FirmwareContent.Files.Select(_fileParser.GetImageFile).ToList();

                // Получаем список секций
                IEnumerable<IDataSection<TMemoryKind>> dataSections = GetDataSections(Project, moduleProject, firmwareFiles);

                // Записываем секции в соответствующие буферы
                foreach (var dataSection in dataSections)
                    dataSection.WriteTo(buffers[dataSection.Placement.MemoryKind]);

                // Создание образа из буферов
                return CreateImage(buffers);
            }
        }

        /// <summary>Перечисляет все секции данных в образе</summary>
        /// <param name="Project">Проект образа</param>
        /// <param name="ModuleProject"></param>
        /// <param name="FirmwareFiles"></param>
        protected abstract IEnumerable<IDataSection<TMemoryKind>> GetDataSections(FirmwareProject Project, ModuleProject ModuleProject,
                                                                                  IList<BinaryImageFile<TMemoryKind>> FirmwareFiles);

        /// <summary>Перечислите типы памяти, присутствующие в образе</summary>
        protected abstract IEnumerable<TMemoryKind> EnumerateMemoryKinds();

        /// <summary>Создайте экземпляр образа прошивки</summary>
        /// <param name="Buffers">Буферы для всех перечисленных в <see cref="EnumerateMemoryKinds" /> типов памяти</param>
        protected abstract TImage CreateImage(IDictionary<TMemoryKind, IBuffer> Buffers);
    }
}
