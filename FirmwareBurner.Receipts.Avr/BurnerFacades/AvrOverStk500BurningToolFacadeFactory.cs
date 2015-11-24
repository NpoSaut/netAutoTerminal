using AsyncOperations.Progress;
using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;
using FirmwareBurner.BurningTools.Stk500;
using FirmwareBurner.ImageFormatters.Avr;

namespace FirmwareBurner.Receipts.Avr.BurnerFacades
{
    [BurningReceiptFactory("Прошить через AVRISP (Драйвер Atmel Studio)")]
    [TargetDevice("at90can128"), TargetDevice("at90can64")]
    public class AvrOverStk500BurningToolFacadeFactory : IBurningToolFacadeFactory<AvrImage>
    {
        private readonly Stk500BurningToolFactory _burningToolFactory;
        private readonly IProgressControllerFactory _progressControllerFactory;

        public AvrOverStk500BurningToolFacadeFactory(Stk500BurningToolFactory BurningToolFactory, IProgressControllerFactory ProgressControllerFactory)
        {
            _burningToolFactory = BurningToolFactory;
            _progressControllerFactory = ProgressControllerFactory;
        }

        public IBurningToolFacade<AvrImage> GetBurningToolFacade(string DeviceName)
        {
            return new AvrOverStk500BurningToolFacade(_burningToolFactory, DeviceName, _progressControllerFactory);
        }
    }
}
