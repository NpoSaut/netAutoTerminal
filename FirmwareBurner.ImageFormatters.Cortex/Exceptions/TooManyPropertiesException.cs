using System;
using System.Runtime.Serialization;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Cortex.Exceptions
{
    /// <Summary>Загрузчик не поддерживает такое количество свойств в образе</Summary>
    [Serializable]
    public class TooManyPropertiesException : ImageFormatterException
    {
        public TooManyPropertiesException() : base("Загрузчик не поддерживает такое количество свойств в образе") { }

        protected TooManyPropertiesException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
