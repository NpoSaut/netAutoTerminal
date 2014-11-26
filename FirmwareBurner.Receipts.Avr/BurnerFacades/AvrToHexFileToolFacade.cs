using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.IntelHex;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    public class AvrToHexFileToolFacade : IBurningToolFacade<AvrImage>
    {
        private const string FlashFileName = @"D:\flash.hex";
        private const string EepromFileName = @"D:\eeprom.hex";

        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        public void Burn(AvrImage Image)
        {
            IntelHexStream flashHexStream = new IntelHexStream(),
                           eepromHexStream = new IntelHexStream();

            Image.FlashBuffer.CopyTo(flashHexStream);
            Image.EepromBuffer.CopyTo(eepromHexStream);

            flashHexStream.GetHexFile().SaveTo(FlashFileName);
            eepromHexStream.GetHexFile().SaveTo(EepromFileName);
        }
    }
}
