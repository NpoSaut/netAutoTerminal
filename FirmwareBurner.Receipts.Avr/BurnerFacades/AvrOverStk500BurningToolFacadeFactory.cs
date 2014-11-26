using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    [BurningReceiptFactory("Прошить через STK500")]
    [TargetDevice("at90can128"), TargetDevice("at90can64")]
    public class AvrOverStk500BurningToolFacadeFactory : IBurningToolFacadeFactory<AvrImage>
    {
        public IBurningToolFacade<AvrImage> GetBurningToolFacade(string DeviceName) { return new AvrOverStk500BurningToolFacade(DeviceName); }
    }
}
