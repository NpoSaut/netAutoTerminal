using System;
using System.Runtime.Serialization;

namespace Saut.AutoTerminal.Exceptions
{
    /// <Summary> Поток терминала закончился </Summary>
    [Serializable]
    public class EndOfStreamTerminalException : TerminalException
    {
         
        public EndOfStreamTerminalException() : base("Поток терминала закончился") { }

        protected EndOfStreamTerminalException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}