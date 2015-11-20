using System;
using System.Runtime.Serialization;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Cortex.Exceptions
{
    /// <Summary>Загрузчик не поддерживает такое количество файлов</Summary>
    [Serializable]
    public class TooManyFilesException : ImageFormatterException
    {
        public TooManyFilesException() : base("Загрузчик не поддерживает такое количество файлов") { }

        protected TooManyFilesException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
