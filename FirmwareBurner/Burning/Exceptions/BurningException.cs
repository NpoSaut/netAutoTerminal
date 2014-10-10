using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.Burning.Exceptions
{
    [Serializable]
    public class BurningException : Exception
    {
        public BurningException() { }
        public BurningException(string message) : base(message) { }
        public BurningException(string message, Exception inner) : base(message, inner) { }

        protected BurningException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}
