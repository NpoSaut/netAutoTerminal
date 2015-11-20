using System;
using System.Runtime.Serialization;

namespace FirmwarePacker.Exceptions
{
    /// <Summary>Ошибка в проектном файле</Summary>
    [Serializable]
    public class BadProjectFileException : ApplicationException
    {
        public BadProjectFileException(Exception inner) : base(string.Format("Ошибка в проектном файле: {0}", inner.Message), inner) { }

        protected BadProjectFileException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
