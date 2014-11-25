using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    [BurnsDevice("at90can128"), BurnsDevice("at90can64")]
    public class AvrOverStk500BurningToolFacadeFactory : IBurningToolFacadeFactory<AvrImage>
    {
        public IBurningToolFacade<AvrImage> GetBurningToolFacade(string DeviceName) { return new AvrOverStk500BurningToolFacade(DeviceName); }
    }
}