using System;
using System.Runtime.Serialization;
using FirmwareBurner.Burning.Exceptions;

namespace FirmwareBurner.Burning.Burners.AvrIsp
{
    /// <summary>Программатор не подключен</summary>
    [Serializable]
    public class ProgrammerIsNotConnectedException : BurningException
    {
        public ProgrammerIsNotConnectedException() : base("Программатор не подключен") { }
        public ProgrammerIsNotConnectedException(string message) : base(message) { }
        public ProgrammerIsNotConnectedException(string message, Exception inner) : base(message, inner) { }

        protected ProgrammerIsNotConnectedException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}
