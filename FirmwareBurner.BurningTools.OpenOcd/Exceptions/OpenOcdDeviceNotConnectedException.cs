using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.OpenOcd.Exceptions
{
    /// <Summary>Ячейка не подключена</Summary>
    [Serializable]
    public class OpenOcdDeviceNotConnectedException : OpenOcdException
    {
        public OpenOcdDeviceNotConnectedException() : base("Ячейка не подключена") { }

        protected OpenOcdDeviceNotConnectedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
