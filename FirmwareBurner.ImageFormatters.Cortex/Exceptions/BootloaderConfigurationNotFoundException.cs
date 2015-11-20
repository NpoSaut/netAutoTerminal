using System;
using System.Runtime.Serialization;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Cortex.Exceptions
{
    /// <Summary>Не найдена конфигурация загрузчика для указанной ячейки</Summary>
    [Serializable]
    public class BootloaderConfigurationNotFoundException : ImageFormatterException
    {
        public BootloaderConfigurationNotFoundException() : base("Не найдена конфигурация загрузчика для указанной ячейки") { }

        protected BootloaderConfigurationNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
