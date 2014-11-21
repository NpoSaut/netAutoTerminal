using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.Stk500.Exceptions
{
    /// <summary>Программатор не подключен</summary>
    [Serializable]
    public class ProgrammerIsNotConnectedException : Stk500Exception
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
