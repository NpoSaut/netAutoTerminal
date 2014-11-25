using FirmwareBurner.Burning;
using FirmwareBurner.ImageFormatters.Avr;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    public class AvrOverStk500BurningToolFacadeFactory : IBurningToolFacadeFactory<AvrImage>
    {
        public IBurningToolFacade<AvrImage> GetBurningToolFacade(string DeviceName) { return new AvrOverStk500BurningToolFacade(DeviceName); }
    }
}
