using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Interaction;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    [BurningReceiptFactory("Сохранить в .hex файл")]
    [TargetDevice("at90can128"), TargetDevice("at90can64")]
    public class AvrToHexFileToolFacadeFactory : IBurningToolFacadeFactory<AvrImage>
    {
        private readonly IFileSelectorService _fileSelectorService;
        public AvrToHexFileToolFacadeFactory(IFileSelectorService FileSelectorService) { _fileSelectorService = FileSelectorService; }

        /// <summary>Создаёт фасад взаимодействия с инструментом прошивки для указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        public IBurningToolFacade<AvrImage> GetBurningToolFacade(string DeviceName) { return new AvrToHexFileToolFacade(_fileSelectorService); }
    }
}
