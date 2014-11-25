using FirmwareBurner.Burning;
using FirmwareBurner.BurningTools.Stk500;
using FirmwareBurner.BurningTools.Stk500.Utilities;
using FirmwareBurner.ImageFormatters.Avr;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    /// <summary>Рецеп по прошивке <see cref="AvrImage" /> через <see cref="Stk500BurningTool" />
    /// </summary>
    public class AvrOverStk500BurningToolFacade : IBurningToolFacade<AvrImage>
    {
        private readonly string _deviceName;
        public AvrOverStk500BurningToolFacade(string DeviceName) { _deviceName = DeviceName; }

        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        public void Burn(AvrImage Image)
        {
            using (var burner = new Stk500BurningTool(_deviceName))
            {
                var fuses = new Fuses { FuseH = Image.Fuses.FuseH, FuseL = Image.Fuses.FuseL, FuseE = Image.Fuses.FuseX };
                burner.WriteFuse(fuses);
                using (TemporaryFile flashFile = new TemporaryFile(Image.FlashBuffer),
                                     eepromFile = new TemporaryFile(Image.EepromBuffer))
                {
                    burner.WriteFlash(flashFile.FileInfo);
                    burner.WriteFlash(eepromFile.FileInfo);
                }
            }
        }
    }
}
