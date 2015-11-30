using AsyncOperations.Progress;
using ExternalTools.Implementations;
using ExternalTools.Interfaces;
using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;
using FirmwareBurner.BurningTools.AvrDude;
using FirmwareBurner.BurningTools.AvrDude.Parameters;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Receipts.Avr.Tools;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    [BurningReceiptFactory("Прошить через AVRISP (Драйвер LibUSB)")]
    [TargetDevice("at90can128"), TargetDevice("at90can64")]
    public class AvrOverAvrDudeAvrIspBurningToolFacadeFactory : IBurningToolFacadeFactory<AvrImage>
    {
        private readonly AvrDudeBurningToolFactory _burningToolFactory;

        public AvrOverAvrDudeAvrIspBurningToolFacadeFactory(IToolLauncher ToolLauncher, IProgressControllerFactory ProgressControllerFactory,
                                                            IAvrDudeChipPseudonameProvider ChipPseudonameProvider)
        {
            _burningToolFactory =
                _burningToolFactory = new AvrDudeBurningToolFactory(ToolLauncher, ChipPseudonameProvider, ProgressControllerFactory,
                                                                    new StaticToolBodyFactory(@"Tools\AvrDude", "avrdude.exe"));
        }

        /// <summary>Создаёт фасад взаимодействия с инструментом прошивки для указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        public IBurningToolFacade<AvrImage> GetBurningToolFacade(string DeviceName)
        {
            return new AvrOverAvrDudeBurningToolFacade(DeviceName, _burningToolFactory, new ConstantProgrammerTypeSelector(ProgrammerType.AvrIsp));
        }
    }
}
