using System;
using System.Runtime.Serialization;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Binary.Exceptions
{
    /// <Summary>Отсутствует файл с загрузчиком</Summary>
    [Serializable]
    public class BootloaderBodyNotFoundException : ImageFormatterException
    {
        public BootloaderBodyNotFoundException() : base("Отсутствует файл с загрузчиком") { }

        protected BootloaderBodyNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
