using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.OpenOcd.Exceptions
{
    /// <Summary>Не найден файл конфигурации, необходимый для прошивки</Summary>
    [Serializable]
    public class OpenOcdConfigurationFileNotFoundException : OpenOcdException
    {
        public OpenOcdConfigurationFileNotFoundException(string FileName, string Output)
            : base(string.Format("Не найден файл конфигурации, необходимый для прошивки ({0})", FileName), Output) { }

        protected OpenOcdConfigurationFileNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
