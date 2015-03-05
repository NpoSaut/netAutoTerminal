using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.Burning.Exceptions
{
    /// <summary>Исключение во время прошивки образа</summary>
    [Serializable]
    public class BurningException : Exception
    {
        public BurningException(Exception inner) : base("Исключение во время прошивки образа", inner) { }

        protected BurningException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}
