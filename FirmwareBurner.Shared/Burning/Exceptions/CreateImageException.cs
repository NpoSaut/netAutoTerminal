using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.Burning.Exceptions
{
    /// <Summary>Исключение при составлении образа</Summary>
    [Serializable]
    public class CreateImageException : ApplicationException
    {
        public CreateImageException(Exception inner) : base("Исключение при составлении образа", inner) { }

        protected CreateImageException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
