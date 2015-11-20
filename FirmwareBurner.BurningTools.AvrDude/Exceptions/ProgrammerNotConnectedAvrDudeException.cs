using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.AvrDude.Exceptions
{
    /// <Summary>Программатор не подключен к компьютеру</Summary>
    [Serializable]
    public class ProgrammerNotConnectedAvrDudeException : AvrDudeException
    {
        public ProgrammerNotConnectedAvrDudeException(String Output) : base("Программатор не подключен к компьютеру", Output) { }

        protected ProgrammerNotConnectedAvrDudeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
