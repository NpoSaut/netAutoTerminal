using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.AvrDude.Exceptions
{
    /// <Summary>Исключение при работе с программой AVRDude</Summary>
    [Serializable]
    public class AvrDudeException : ApplicationException
    {
        public AvrDudeException() : base("Исключение при работе с программой AVRDude") { }
        public AvrDudeException(Exception inner) : base("Исключение при работе с программой AVRDude", inner) { }
        public AvrDudeException(string message) : base(message) { }
        public AvrDudeException(string message, Exception inner) : base(message, inner) { }

        protected AvrDudeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }

        public String Output { get; set; }
    }
}
