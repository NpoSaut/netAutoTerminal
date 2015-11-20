using System;
using System.IO;
using AsyncOperations.Progress;
using ExternalTools;
using FirmwareBurner.Burning;
using FirmwareBurner.BurningTools.Stk500;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.IntelHex;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    /// <summary>Рецеп по прошивке <see cref="AvrImage" /> через <see cref="Stk500BurningTool" />
    /// </summary>
    public class AvrOverStk500BurningToolFacade : IBurningToolFacade<AvrImage>
    {
        const bool EraseBeforeWrite = true;

        private readonly Stk500BurningToolFactory _burningToolFactory;
        private readonly string _chipName;
        private readonly IProgressControllerFactory _progressControllerFactory;

        public AvrOverStk500BurningToolFacade(Stk500BurningToolFactory BurningToolFactory, string ChipName, IProgressControllerFactory ProgressControllerFactory)
        {
            _burningToolFactory = BurningToolFactory;
            _chipName = ChipName;
            _progressControllerFactory = ProgressControllerFactory;
        }

        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        /// <param name="Target">Цель прошивки</param>
        /// <param name="ProgressToken">Токен прогресса выполнения операции</param>
        public void Burn(AvrImage Image, TargetInformation Target, IProgressToken ProgressToken)
        {
            using (IProgressController progress = _progressControllerFactory.CreateController(ProgressToken))
            {
                Stk500BurningTool burner = _burningToolFactory.GetBurningTool(_chipName);
                var fuses = new Fuses { FuseH = Image.Fuses.FuseH, FuseL = Image.Fuses.FuseL, FuseE = Image.Fuses.FuseX };
                
                burner.WriteFuse(fuses);
                WriteBuffer(Image.FlashBuffer, burner.WriteFlash);
                WriteBuffer(Image.EepromBuffer, burner.WriteEeprom);
            }
        }

        private void WriteBuffer(IBuffer Buffer, Action<FileInfo, bool> WriteMethod)
        {
            if (Buffer.IsEmpty) return;
            var hexStream = new IntelHexStream();
            Buffer.CopyTo(hexStream);
            IntelHexFile hexFile = hexStream.GetHexFile();
            using (var file = new TemporaryFile(hexFile.OpenIntelHexStream()))
            {
                WriteMethod(file.FileInfo, EraseBeforeWrite);
            }
        }
    }
}
