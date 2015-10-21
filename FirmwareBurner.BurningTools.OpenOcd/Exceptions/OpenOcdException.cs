using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.OpenOcd.Exceptions
{
    /// <Summary>Ошибка при работе с программой OpenOCD</Summary>
    [Serializable]
    public class OpenOcdException : ApplicationException
    {
        public OpenOcdException(int ErrorCode) : base(string.Format("Ошибка при работе с программой OpenOCD (Код ошибки: {0})", ErrorCode))
        {
            this.ErrorCode = ErrorCode;
        }

        public OpenOcdException(Exception inner) : base("Ошибка при работе с программой OpenOCD", inner) { }

        protected OpenOcdException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }

        public int ErrorCode { get; private set; }
    }
}
