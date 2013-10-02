using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwareBurner.Burning.Exceptions
{
    [Serializable]
    public class BurningException : System.Exception
    {
        public BurningException() { }
        public BurningException(string message) : base(message) { }
        public BurningException(string message, System.Exception inner) : base(message, inner) { }
        protected BurningException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
