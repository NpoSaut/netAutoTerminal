using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;

namespace FirmwareBurner.BurningTools.Stk500.Receipts
{
    /// <summary>Рецеп по прошивке <see cref="AvrImage" /> через <see cref="Stk500BurningFacade" />
    /// </summary>
    public class AvrOverStk500BurningReceipt : IBurningReceipt<AvrImage>
    {
        /// <summary>Название рецепта прошивки</summary>
        /// <remarks>
        ///     Это название будет отображаться в интерфейсе в виде подписи к команде прошивки. Например "Прошить через
        ///     AvrDude" или "Сохранить в файл"
        /// </remarks>
        public string Name
        {
            get { return "Прошить через STK500"; }
        }

        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        public void Burn(AvrImage Image)
        {
            using (var burner = new Stk500BurningFacade())
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
