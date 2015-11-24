using System.Collections.Generic;
using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;
using FirmwareBurner.BurningTools.AvrDude;
using FirmwareBurner.BurningTools.AvrDude.Parameters;
using FirmwareBurner.ImageFormatters.Avr;
using FirmwareBurner.Receipts.Avr.Tools;
using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    [BurningReceiptFactory("Прошить через ARM-USB-TINY-H")]
    [TargetDevice("at90can128"), TargetDevice("at90can64")]
    public class AvrOverAvrDudeArmUsbTinyBurningToolFacadeFactory : IBurningToolFacadeFactory<AvrImage>
    {
        private const ProgrammerType DirectProgrammer = ProgrammerType.ArmUsbTinyH;

        private static readonly IDictionary<int, ProgrammerType> _programmerAssociations =
            new Dictionary<int, ProgrammerType>
            {
                { 1, ProgrammerType.ArmUsbTinyHChannel1 },
                { 2, ProgrammerType.ArmUsbTinyHChannel2 },
            };

        private readonly AvrDudeBurningToolFactory _burningToolFactory;
        private readonly IIndexHelper _indexHelper;

        public AvrOverAvrDudeArmUsbTinyBurningToolFacadeFactory(AvrDudeBurningToolFactory BurningToolFactory, IIndexHelper IndexHelper)
        {
            _burningToolFactory = BurningToolFactory;
            _indexHelper = IndexHelper;
        }

        /// <summary>Создаёт фасад взаимодействия с инструментом прошивки для указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        public IBurningToolFacade<AvrImage> GetBurningToolFacade(string DeviceName)
        {
            return new AvrOverAvrDudeBurningToolFacade(DeviceName, _burningToolFactory,
                                                       new UnichannelProgrammerTypeSelector(_programmerAssociations, DirectProgrammer, _indexHelper));
        }
    }
}
