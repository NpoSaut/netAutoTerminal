using System.Collections.Generic;
using FirmwareBurner.BurningTools.AvrDude.Parameters;
using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.Receipts.Avr.Tools
{
    internal class UnichannelProgrammerTypeSelector : IProgrammerTypeSelector
    {
        private readonly ProgrammerType _directProgrammer;
        private readonly IIndexHelper _indexHelper;
        private readonly IDictionary<int, ProgrammerType> _programmersAssassinations;

        public UnichannelProgrammerTypeSelector(IDictionary<int, ProgrammerType> ProgrammersAssassinations, ProgrammerType DirectProgrammer,
                                                IIndexHelper IndexHelper)
        {
            _programmersAssassinations = ProgrammersAssassinations;
            _directProgrammer = DirectProgrammer;
            _indexHelper = IndexHelper;
        }

        public ProgrammerType GetProgrammerType(TargetInformation Target)
        {
            ModificationKind modification = _indexHelper.GetModification(Target.CellId, Target.ModificationId);
            string unichannelProperty = modification.CustomProperties["unichannel"];
            return unichannelProperty != null && bool.Parse(unichannelProperty)
                       ? _programmersAssassinations[Target.ChannelNumber]
                       : _directProgrammer;
        }
    }
}
