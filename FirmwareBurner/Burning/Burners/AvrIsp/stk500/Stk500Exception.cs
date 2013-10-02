using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwareBurner.Burning.Exceptions;

namespace FirmwareBurner.Burning.Burners.AvrIsp.stk500
{
    [Serializable]
    public class Stk500Exception : BurningException
    {
        public Stk500Exception() { }
        public Stk500Exception(string message) : base(message) { }
        public Stk500Exception(string message, Exception inner) : base(message, inner) { }
        protected Stk500Exception(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
