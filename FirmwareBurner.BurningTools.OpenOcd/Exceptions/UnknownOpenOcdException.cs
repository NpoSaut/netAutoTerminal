using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.OpenOcd.Exceptions
{
    /// <Summary>Неизвестная ошибка при работе с программатором</Summary>
    [Serializable]
    public class UnknownOpenOcdException : OpenOcdException
    {
        public UnknownOpenOcdException(string Output) : base("Неизвестная ошибка при работе с программатором", Output) { }

        protected UnknownOpenOcdException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
