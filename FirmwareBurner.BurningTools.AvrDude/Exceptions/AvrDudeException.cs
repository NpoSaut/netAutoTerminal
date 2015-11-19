using System;
using System.Runtime.Serialization;
using System.Text;

namespace FirmwareBurner.BurningTools.AvrDude.Exceptions
{
    /// <Summary>Исключение при работе с программой AVRDude</Summary>
    [Serializable]
    public class AvrDudeException : ApplicationException
    {
        public AvrDudeException(string message, string Output) : base(message) { this.Output = Output; }

        protected AvrDudeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }

        public String Output { get; private set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine();
            sb.AppendLine("-------------------- Output --------------------");
            sb.AppendLine();
            sb.AppendLine(Output);
            return sb.ToString();
        }
    }
}
