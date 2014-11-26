using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    public class AvrToHexFileToolFacadeFactory : IBurningToolFacadeFactory<AvrImage>
    {
        /// <summary>Создаёт фасад взаимодействия с инструментом прошивки для указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        public IBurningToolFacade<AvrImage> GetBurningToolFacade(string DeviceName) { return new AvrToHexFileToolFacade(); }
    }
}
