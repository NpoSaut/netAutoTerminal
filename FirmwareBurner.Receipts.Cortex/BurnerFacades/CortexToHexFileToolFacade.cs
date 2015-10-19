using System.Collections.ObjectModel;
using AsyncOperations.Progress;
using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Cortex;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.IntelHex;
using FirmwareBurner.Interaction;

namespace FirmwareBurner.Receipts.Cortex.BurnerFacades
{
    public class CortexToHexFileToolFacade : IBurningToolFacade<CortexImage>
    {
        private static readonly Collection<FileRequestArguments.FileTypeDescription> _fileTypeDescriptions =
            new Collection<FileRequestArguments.FileTypeDescription>
            {
                new FileRequestArguments.FileTypeDescription("hex", "Файл IntelHEX")
            };

        private readonly IFileSelectorService _fileSelectorService;

        public CortexToHexFileToolFacade(IFileSelectorService FileSelectorService) { _fileSelectorService = FileSelectorService; }

        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        /// <param name="ProgressToken">Токен прогресса выполнения операции</param>
        public void Burn(CortexImage Image, IProgressToken ProgressToken) { SaveBuffer(Image.FlashBuffer, "flash", "Куда сохранить файл с прошивкой?"); }

        private void SaveBuffer(IBuffer Buffer, string FileName, string Message)
        {
            if (Buffer.IsEmpty) return;
            string fileName = _fileSelectorService.RequestSaveFileLocation(new SaveFileRequestArguments(Message, "hex")
                                                                           {
                                                                               DefaultFileName = FileName,
                                                                               FileTypes = _fileTypeDescriptions,
                                                                           });
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                var hexStream = new IntelHexStream();
                Buffer.CopyTo(hexStream);
                hexStream.GetHexFile().SaveTo(fileName);
            }
        }
    }
}
