using FirmwareBurner.BurningTools.AvrDude.Parameters;

namespace FirmwareBurner.Receipts.Avr.Tools
{
    public interface IProgrammerTypeSelector
    {
        ProgrammerType GetProgrammerType(TargetInformation Target);
    }
}
