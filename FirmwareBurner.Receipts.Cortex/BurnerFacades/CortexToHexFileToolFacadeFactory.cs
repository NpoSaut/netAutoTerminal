using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Cortex;
using FirmwareBurner.Interaction;

namespace FirmwareBurner.Receipts.Cortex.BurnerFacades
{
    [BurningReceiptFactory("Сохранить в .hex файл")]
    [TargetDevice("stm32f4"), TargetDevice("mdr32f9q2i"), TargetDevice("at91sam7a3")]
    public class CortexToHexFileToolFacadeFactory : IBurningToolFacadeFactory<CortexImage>
    {
        private readonly IFileSelectorService _fileSelectorService;
        public CortexToHexFileToolFacadeFactory(IFileSelectorService FileSelectorService) { _fileSelectorService = FileSelectorService; }

        /// <summary>Создаёт фасад взаимодействия с инструментом прошивки для указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        public IBurningToolFacade<CortexImage> GetBurningToolFacade(string DeviceName) { return new CortexToHexFileToolFacade(_fileSelectorService); }
    }
}
