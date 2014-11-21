using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.Stk500.Exceptions
{
    [Serializable]
    public class BurnerNotFoundException : Stk500Exception
    {
        public BurnerNotFoundException() : base("Не найдена программа stk500.exe, необходимая для прошивки модуля") { }
        public BurnerNotFoundException(string message) : base(message) { }
        public BurnerNotFoundException(string message, Exception inner) : base(message, inner) { }

        protected BurnerNotFoundException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}
