using System;
using System.Runtime.Serialization;

namespace Saut.AutoTerminal.Exceptions
{
    /// <Summary> Превышен таймаут ожидания ответа от терминала</Summary>
    [Serializable]
    public class TimeoutTerminalException : TerminalException
    {
        public TimeoutTerminalException() : base("Превышен таймаут ожидания ответа от терминала") { }

        protected TimeoutTerminalException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
