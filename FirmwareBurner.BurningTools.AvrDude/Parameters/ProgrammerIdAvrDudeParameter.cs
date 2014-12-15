using System;
using System.Collections.Generic;

namespace FirmwareBurner.BurningTools.AvrDude.Parameters
{
    public enum ProgrammerType
    {
        AvrIsp
    }

    /// <summary>Параметр идентификатора программатора</summary>
    [ParameterKey('a')]
    public class ProgrammerIdAvrDudeParameter : AvrDudeParameter
    {
        private readonly Dictionary<ProgrammerType, string> _programerIdPseudonames =
            new Dictionary<ProgrammerType, string>
            {
                // ReSharper disable StringLiteralTypo
                { ProgrammerType.AvrIsp, "avrisp2" }
                // ReSharper restore StringLiteralTypo
            };

        private readonly ProgrammerType _programmerType;

        public ProgrammerIdAvrDudeParameter(ProgrammerType ProgrammerType) { _programmerType = ProgrammerType; }

        /// <summary>Значение параметра</summary>
        protected override string Value
        {
            get { return _programerIdPseudonames[_programmerType]; }
        }

        public override string ToString() { return String.Format("Programer Type: {0}", _programmerType); }
    }
}
