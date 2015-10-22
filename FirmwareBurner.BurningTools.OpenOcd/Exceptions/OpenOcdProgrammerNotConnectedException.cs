using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.OpenOcd.Exceptions
{
    /// <Summary>Программатор не подключен</Summary>
    [Serializable]
    public class OpenOcdProgrammerNotConnectedException : OpenOcdException
    {
        public OpenOcdProgrammerNotConnectedException(string Output) : base("Программатор не подключен", Output) { }

        protected OpenOcdProgrammerNotConnectedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
