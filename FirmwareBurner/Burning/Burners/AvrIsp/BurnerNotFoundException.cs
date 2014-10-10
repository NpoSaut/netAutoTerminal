using System;
using System.Runtime.Serialization;
using FirmwareBurner.Burning.Exceptions;

namespace FirmwareBurner.Burning.Burners.AvrIsp
{
    [Serializable]
    public class BurnerNotFoundException : BurningException
    {
        public BurnerNotFoundException() : base("Не наидена программа stk500.exe, необходимая для прошивки модуля") { }
        public BurnerNotFoundException(string message) : base(message) { }
        public BurnerNotFoundException(string message, Exception inner) : base(message, inner) { }

        protected BurnerNotFoundException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}
