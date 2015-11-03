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

        /// <summary>���������� ����� ��� ���������� ������� ��������</summary>
        /// <param name="Project">������ ������</param>
        /// <param name="ProgressToken">����� ��������� ���������� ��������</param>
        /// <returns>����� ��������</returns>
        public TImage GetImage(FirmwareProject Project, IProgressToken ProgressToken)
        {
            using (_progressControllerFactory.CreateController(ProgressToken))
            {
                // ���������� �������
                Dictionary<TMemoryKind, IBuffer> buffers = EnumerateMemoryKinds().ToDictionary(mk => mk, mk => _bufferFactory.CreateBuffer());

                ModuleProject moduleProject = Project.Modules.Single();
                IList<BinaryImageFile<TMemoryKind>> firmwareFiles = moduleProject.FirmwareContent.Files.Select(_fileParser.GetImageFile).ToList();

                // �������� ������ ������
                IEnumerable<IDataSection<TMemoryKind>> dataSections = GetDataSections(Project, moduleProject, firmwareFiles);

                // ���������� ������ � ��������������� ������
                foreach (var dataSection in dataSections)
                    dataSection.WriteTo(buffers[dataSection.Placement.MemoryKind]);

                // �������� ������ �� �������
                return CreateImage(buffers);
            }
        }

        /// <summary>����������� ��� ������ ������ � ������</summary>
        /// <param name="Project">������ ������</param>
        /// <param name="ModuleProject"></param>
        /// <param name="FirmwareFiles"></param>
        protected abstract IEnumerable<IDataSection<TMemoryKind>> GetDataSections(FirmwareProject Project, ModuleProject ModuleProject,
                                                                                  IList<BinaryImageFile<TMemoryKind>> FirmwareFiles);

        /// <summary>����������� ���� ������, �������������� � ������</summary>
        protected abstract IEnumerable<TMemoryKind> EnumerateMemoryKinds();

        /// <summary>�������� ��������� ������ ��������</summary>
        /// <param name="Buffers">������ ��� ���� ������������� � <see cref="EnumerateMemoryKinds" /> ����� ������</param>
        protected abstract TImage CreateImage(IDictionary<TMemoryKind, IBuffer> Buffers);
    }
}
