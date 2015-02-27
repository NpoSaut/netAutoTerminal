using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.BurningTools.AvrDude.Exceptions
{
    /// <Summary>Программируемое устройство не подключено</Summary>
    [Serializable]
    public class DeviceNotConnectedAvrDudeException : AvrDudeException
    {
        public DeviceNotConnectedAvrDudeException(String Output) : base("Программируемое устройство не подключено") { this.Output = Output; }

        protected DeviceNotConnectedAvrDudeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
