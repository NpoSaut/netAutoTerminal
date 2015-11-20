using System;
using System.Runtime.Serialization;
using System.Text;

namespace FirmwareBurner.BurningTools.OpenOcd.Exceptions
{
    /// <Summary>Ошибка при работе с программой OpenOCD</Summary>
    [Serializable]
    public class OpenOcdException : ApplicationException
    {
        public OpenOcdException(int ErrorCode, string Output)
            : base(string.Format("Ошибка при работе с программой OpenOCD (Код ошибки: {0})", ErrorCode))
        {
            this.ErrorCode = ErrorCode;
            this.Output = Output;
        }

        public OpenOcdException(string Message, string Output) : base(Message) { this.Output = Output; }
        public OpenOcdException(Exception inner) : base("Ошибка при работе с программой OpenOCD", inner) { }

        protected OpenOcdException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }

        public string Output { get; private set; }

        public int ErrorCode { get; private set; }

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
