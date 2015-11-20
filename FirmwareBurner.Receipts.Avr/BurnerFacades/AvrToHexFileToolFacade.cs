using System.Collections.ObjectModel;
using AsyncOperations.Progress;
using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.IntelHex;
using FirmwareBurner.Interaction;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    public class AvrToHexFileToolFacade : IBurningToolFacade<AvrImage>
    {
        private static Collection<FileRequestArguments.FileTypeDescription> _fileTypeDescriptions;
        private readonly IFileSelectorService _fileSelectorService;

        public AvrToHexFileToolFacade(IFileSelectorService FileSelectorService) { _fileSelectorService = FileSelectorService; }

        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        /// <param name="Target">Цель прошивки</param>
        /// <param name="ProgressToken">Токен прогресса выполнения операции</param>
        public void Burn(AvrImage Image, TargetInformation Target, IProgressToken ProgressToken)
        {
            SaveBuffer(Image.FlashBuffer, "flash", "Куда сохранить файл с Flash?");
            SaveBuffer(Image.EepromBuffer, "eeprom", "Куда сохранить файл с EEPROM?");
        }

        private void SaveBuffer(IBuffer Buffer, string FileName, string Message)
        {
            if (Buffer.IsEmpty) return;
            _fileTypeDescriptions = new Collection<FileRequestArguments.FileTypeDescription>
                                    {
                                        new FileRequestArguments.FileTypeDescription("hex", "Файл IntelHEX")
                                    };
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
