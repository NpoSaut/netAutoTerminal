using FirmwareBurner.Burning;
using FirmwareBurner.BurningTools.Stk500;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.ImageFormatters.Avr.Utilities;
using FirmwareBurner.IntelHex;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    /// <summary>Рецеп по прошивке <see cref="AvrImage" /> через <see cref="Stk500BurningTool" />
    /// </summary>
    public class AvrOverStk500BurningToolFacade : IBurningToolFacade<AvrImage>
    {
        private readonly Stk500BurningTool _burner;
        public AvrOverStk500BurningToolFacade(Stk500BurningTool Burner) { _burner = Burner; }

        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        public void Burn(AvrImage Image)
        {
            var fuses = new Fuses { FuseH = Image.Fuses.FuseH, FuseL = Image.Fuses.FuseL, FuseE = Image.Fuses.FuseX };
            _burner.WriteFuse(fuses);

            IntelHexStream flashHexStream = new IntelHexStream(),
                           eepromHexStream = new IntelHexStream();

            Image.FlashBuffer.CopyTo(flashHexStream);
            Image.EepromBuffer.CopyTo(eepromHexStream);

            using (TemporaryFile flashFile = new TemporaryFile(flashHexStream),
                                 eepromFile = new TemporaryFile(eepromHexStream))
            {
                _burner.WriteFlash(flashFile.FileInfo);
                _burner.WriteFlash(eepromFile.FileInfo);
            }
        }
    }
}
