using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;
using FirmwareBurner.BurningTools.AvrDude;
using FirmwareBurner.BurningTools.AvrDude.Parameters;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Receipts.Avr.Tools;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    [BurningReceiptFactory("Прошить через AVRISP (Драйвер WinUSB)")]
    [TargetDevice("at90can128"), TargetDevice("at90can64")]
    public class AvrOverAvrDudeAvrIspBurningToolFacadeFactory : IBurningToolFacadeFactory<AvrImage>
    {
        private readonly AvrDudeBurningToolFactory _burningToolFactory;
        public AvrOverAvrDudeAvrIspBurningToolFacadeFactory(AvrDudeBurningToolFactory BurningToolFactory) { _burningToolFactory = BurningToolFactory; }

        /// <summary>Создаёт фасад взаимодействия с инструментом прошивки для указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        public IBurningToolFacade<AvrImage> GetBurningToolFacade(string DeviceName)
        {
            return new AvrOverAvrDudeBurningToolFacade(DeviceName, _burningToolFactory, new ConstantProgrammerTypeSelector(ProgrammerType.AvrIsp));
        }
    }
}
