using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;
using FirmwareBurner.BurningTools.Stk500;
using FirmwareBurner.ImageFormatters.Avr;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    [BurningReceiptFactory("Прошить через STK500")]
    [TargetDevice("at90can128"), TargetDevice("at90can64")]
    public class AvrOverStk500BurningToolFacadeFactory : IBurningToolFacadeFactory<AvrImage>
    {
        private readonly Stk500BurningToolFactory _burningToolFactory;
        public AvrOverStk500BurningToolFacadeFactory(Stk500BurningToolFactory BurningToolFactory) { _burningToolFactory = BurningToolFactory; }

        public IBurningToolFacade<AvrImage> GetBurningToolFacade(string DeviceName)
        {
            return new AvrOverStk500BurningToolFacade(_burningToolFactory, DeviceName);
        }
    }
}
