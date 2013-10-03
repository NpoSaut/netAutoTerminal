using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwareBurner.Burning.Exceptions;

namespace FirmwareBurner.Burning.Burners.AvrIsp
{
    [Serializable]
    public class DeviceIsNotConnectedException : BurningException
    {
        public DeviceIsNotConnectedException() : base("Программируемая ячейка не подключена") { }
        public DeviceIsNotConnectedException(string message) : base(message) { }
        public DeviceIsNotConnectedException(string message, Exception inner) : base(message, inner) { }
        protected DeviceIsNotConnectedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
