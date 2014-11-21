﻿using System;
using System.Runtime.Serialization;
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
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}