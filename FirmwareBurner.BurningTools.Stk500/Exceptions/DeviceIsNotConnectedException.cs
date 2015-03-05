using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.Stk500.Exceptions
{
    /// <Summary>Программируемое устройство не подключено</Summary>
    [Serializable]
    public class DeviceIsNotConnectedException : Stk500Exception
    {
        public DeviceIsNotConnectedException() : base("Программируемое устройство не подключено") { }
        public DeviceIsNotConnectedException(Exception inner) : base("Программируемое устройство не подключено", inner) { }
        public DeviceIsNotConnectedException(string message) : base(message) { }
        public DeviceIsNotConnectedException(string message, Exception inner) : base(message, inner) { }

        protected DeviceIsNotConnectedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
