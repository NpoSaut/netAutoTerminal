using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwareBurner.Burning.Exceptions;

namespace FirmwareBurner.Burning.Burners.AvrIsp
{
    /// <summary>
    /// Программатор не подключен
    /// </summary>
    [Serializable]
    public class ProgrammerIsNotConnectedException : BurningException
    {
        public ProgrammerIsNotConnectedException() : base("Программатор не подключен") { }
        public ProgrammerIsNotConnectedException(string message) : base(message) { }
        public ProgrammerIsNotConnectedException(string message, Exception inner) : base(message, inner) { }
        protected ProgrammerIsNotConnectedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
