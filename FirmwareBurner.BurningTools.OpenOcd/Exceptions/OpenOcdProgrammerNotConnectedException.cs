using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.OpenOcd.Exceptions
{
    /// <Summary>Программатор не подключен</Summary>
    [Serializable]
    public class OpenOcdProgrammerNotConnectedException : OpenOcdException
    {
        public OpenOcdProgrammerNotConnectedException() : base("Программатор не подключен") { }

        protected OpenOcdProgrammerNotConnectedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
