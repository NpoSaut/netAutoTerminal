using System;

namespace FirmwareBurner.BurningTools.AvrDude.Parameters
{
    /// <summary>Название чипа</summary>
    [ParameterKey('p')]
    public class ChipIdAvrDudeParameter : AvrDudeParameter
    {
        private readonly string _chipName;
        public ChipIdAvrDudeParameter(string ChipName) { _chipName = ChipName; }

        /// <summary>Значение параметра</summary>
        protected override string Value
        {
            get { return _chipName; }
        }

        public override string ToString() { return String.Format("Chip Name: {0}", _chipName); }
    }
}
