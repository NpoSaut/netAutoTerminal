using FirmwareBurner.BurningTools.AvrDude.Parameters;

namespace FirmwareBurner.Receipts.Avr.Tools
{
    public class ConstantProgrammerTypeSelector : IProgrammerTypeSelector
    {
        private readonly ProgrammerType _programmer;
        public ConstantProgrammerTypeSelector(ProgrammerType Programmer) { _programmer = Programmer; }
        public ProgrammerType GetProgrammerType(TargetInformation Target) { return _programmer; }
    }
}
