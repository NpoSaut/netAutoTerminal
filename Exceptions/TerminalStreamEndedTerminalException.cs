using System;
using System.Runtime.Serialization;
using System.Text;

namespace Saut.AutoTerminal.Exceptions
{
    /// <Summary>Поток данных с терминала неожиданно закончился</Summary>
    [Serializable]
    public class TerminalStreamEndedTerminalException : TerminalException
    {
        public const string ExceptionMessage = "Поток данных с терминала неожиданно закончился";
        public TerminalStreamEndedTerminalException(string Log) : base(ExceptionMessage) { this.Log = Log; }

        protected TerminalStreamEndedTerminalException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }

        public string Log { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine();
            sb.AppendLine("Output:");
            sb.AppendLine(Log);

            return sb.ToString();
        }
    }
}
