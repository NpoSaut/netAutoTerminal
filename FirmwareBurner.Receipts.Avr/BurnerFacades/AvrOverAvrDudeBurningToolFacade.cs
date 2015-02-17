using FirmwareBurner.Burning;
using FirmwareBurner.BurningTools.AvrDude;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.IntelHex;
using FirmwareBurner.Progress;
using FirmwareBurner.Receipts.Avr.Utilities;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    public class AvrOverAvrDudeBurningToolFacade : IBurningToolFacade<AvrImage>
    {
        private readonly AvrDudeBurningToolFactory _burningToolFactory;
        private readonly string _chipName;

        public AvrOverAvrDudeBurningToolFacade(string ChipName, AvrDudeBurningToolFactory BurningToolFactory)
        {
            _chipName = ChipName;
            _burningToolFactory = BurningToolFactory;
        }

        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        /// <param name="ProgressToken"></param>
        public void Burn(AvrImage Image, IProgressToken ProgressToken)
        {
            AvrDudeBurningTool burner = _burningToolFactory.GetBurningTool(_chipName);
            var fuses = new Fuses(Image.Fuses.FuseH, Image.Fuses.FuseL, Image.Fuses.FuseX);
            burner.WriteFuse(fuses);

            IntelHexStream flashHexStream = new IntelHexStream(),
                           eepromHexStream = new IntelHexStream();

            Image.FlashBuffer.CopyTo(flashHexStream);
            Image.EepromBuffer.CopyTo(eepromHexStream);

            using (TemporaryFile flashFile = new TemporaryFile(flashHexStream),
                                 eepromFile = new TemporaryFile(eepromHexStream))
            {
                burner.WriteFlash(flashFile.FileInfo);
                burner.WriteFlash(eepromFile.FileInfo);
            }
        }
    }
}
