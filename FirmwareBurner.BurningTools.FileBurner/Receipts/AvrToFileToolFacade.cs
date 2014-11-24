using System;
using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.ImageFormatters.Avr.BurnerFacades;

namespace FirmwareBurner.BurningTools.FileBurner.Receipts
{
    public class AvrToFileToolFacade : IBurningToolFacade<AvrImage>
    {
        /// <summary>Название рецепта прошивки</summary>
        /// <remarks>
        ///     Это название будет отображаться в интерфейсе в виде подписи к команде прошивки. Например "Прошить через
        ///     AvrDude" или "Сохранить в файл"
        /// </remarks>
        public string Name
        {
            get { return "Сохранить в файл"; }
        }

        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        public void Burn(AvrImage Image) { throw new NotImplementedException(); }
    }
}
