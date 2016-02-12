using System;
using System.Runtime.Serialization;

namespace Saut.AutoTerminal.Exceptions
{
    /// <Summary> Ошибка при работе с терминалом </Summary>
    [Serializable]
    public class TerminalException : ApplicationException
    {
        public TerminalException(string Message) : base(Message) { }
        public TerminalException(string Message, Exception inner) : base(Message, inner) { }

        protected TerminalException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
