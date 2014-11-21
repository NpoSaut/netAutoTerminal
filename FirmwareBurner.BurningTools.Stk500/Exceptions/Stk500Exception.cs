using System;
using System.Runtime.Serialization;
using FirmwareBurner.Burning.Exceptions;

namespace FirmwareBurner.BurningTools.Stk500.Exceptions
{
    [Serializable]
    public class Stk500Exception : BurningException
    {
        public Stk500Exception() { }
        public Stk500Exception(string message) : base(message) { }
        public Stk500Exception(string message, Exception inner) : base(message, inner) { }

        protected Stk500Exception(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}
