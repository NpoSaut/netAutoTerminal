using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.OpenOcd.Exceptions
{
    /// <Summary>Не найден файл конфигурации, необходимый для прошивки</Summary>
    [Serializable]
    public class OpenOcdConfigurationFileNotFoundException : OpenOcdException
    {
        public OpenOcdConfigurationFileNotFoundException(string FileName)
            : base(string.Format("Не найден файл конфигурации, необходимый для прошивки ({0})", FileName)) { }

        protected OpenOcdConfigurationFileNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
