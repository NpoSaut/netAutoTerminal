using System;
using System.IO;
using AsyncOperations.Progress;
using ExternalTools;
using FirmwareBurner.Burning;
using FirmwareBurner.BurningTools.AvrDude;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Imaging.Binary.Buffers;
using FirmwareBurner.IntelHex;
using FirmwareBurner.Receipts.Avr.Tools;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    public class AvrOverAvrDudeBurningToolFacade : IBurningToolFacade<AvrImage>
    {
        private readonly AvrDudeBurningToolFactory _burningToolFactory;
        private readonly string _chipName;
        private readonly IProgrammerTypeSelector _programmerTypeSelector;

        public AvrOverAvrDudeBurningToolFacade(string ChipName, AvrDudeBurningToolFactory BurningToolFactory, IProgrammerTypeSelector ProgrammerTypeSelector)
        {
            _chipName = ChipName;
            _burningToolFactory = BurningToolFactory;
            _programmerTypeSelector = ProgrammerTypeSelector;
        }

        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        /// <param name="Target">Цель прошивки</param>
        /// <param name="ProgressToken"></param>
        public void Burn(AvrImage Image, TargetInformation Target, IProgressToken ProgressToken)
        {
            AvrDudeBurningTool burner = _burningToolFactory.GetBurningTool(_chipName, _programmerTypeSelector.GetProgrammerType(Target));
            var fuses = new Fuses(Image.Fuses.FuseH, Image.Fuses.FuseL, Image.Fuses.FuseX);

            var fuseToken = new SubprocessProgressToken(50);
            var flashToken = new SubprocessProgressToken(Image.FlashBuffer.Size);
            var eepromToken = new SubprocessProgressToken(Image.EepromBuffer.Size);

            using (new CompositeProgressManager(ProgressToken, fuseToken, flashToken, eepromToken))
            {
                burner.WriteFuse(fuses, fuseToken);
                WriteBuffer(Image.FlashBuffer, burner.WriteFlash, flashToken);
                WriteBuffer(Image.EepromBuffer, burner.WriteEeprom, eepromToken);
            }
        }

        private void WriteBuffer(IBuffer Buffer, Action<FileInfo, IProgressToken> WriteMethod, IProgressToken ProgressToken)
        {
            if (Buffer.IsEmpty) return;
            var hexStream = new IntelHexStream();
            Buffer.CopyTo(hexStream);
            IntelHexFile hexFile = hexStream.GetHexFile();
            using (var file = new TemporaryFile(hexFile.OpenIntelHexStream()))
            {
                WriteMethod(file.FileInfo, ProgressToken);
            }
        }
    }
}
