using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.Stk500.Exceptions
{
    [Serializable]
    public class Stk500Exception : Exception
    {
        public Stk500Exception() { }
        public Stk500Exception(string message) : base(message) { }
        public Stk500Exception(string message, Exception inner) : base(message, inner) { }

        public String Output { get; set; }

        protected Stk500Exception(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}
