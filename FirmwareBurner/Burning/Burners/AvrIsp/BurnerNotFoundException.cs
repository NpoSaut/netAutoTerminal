using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwareBurner.Burning.Burners.AvrIsp
{
    [Serializable]
    public class BurnerNotFoundException : Burning.Exceptions.BurningException
    {
        public BurnerNotFoundException() : base("Не наидена программа stk500.exe, необходимая для прошивки модуля") { }
        public BurnerNotFoundException(string message) : base(message) { }
        public BurnerNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected BurnerNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
