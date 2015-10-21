using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;
using FirmwareBurner.BurningTools.OpenOcd;
using FirmwareBurner.ImageFormatters.Cortex;
using FirmwareBurner.Receipts.Cortex.Tools;

namespace FirmwareBurner.Receipts.Cortex.BurnerFacades
{
    [BurningReceiptFactory("Прошить через OpenOCD")]
    [TargetDevice("stm32f4"), TargetDevice("mdr32f9q2i"), TargetDevice("at91sam7a3")]
    public class CortexOverOpenOcdToolFacadeFactory : IBurningToolFacadeFactory<CortexImage>
    {
        private readonly IOcdBurningParametersProvider _ocdBurningParametersProvider;
        private readonly OpenOcdToolFactory _openOcdToolFactory;

        public CortexOverOpenOcdToolFacadeFactory(OpenOcdToolFactory OpenOcdToolFactory, IOcdBurningParametersProvider OcdBurningParametersProvider)
        {
            _openOcdToolFactory = OpenOcdToolFactory;
            _ocdBurningParametersProvider = OcdBurningParametersProvider;
        }

        /// <summary>Создаёт фасад взаимодействия с инструментом прошивки для указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        public IBurningToolFacade<CortexImage> GetBurningToolFacade(string DeviceName)
        {
            return new CortexOverOpenOcdToolFacade(_openOcdToolFactory, _ocdBurningParametersProvider);
        }
    }
}
