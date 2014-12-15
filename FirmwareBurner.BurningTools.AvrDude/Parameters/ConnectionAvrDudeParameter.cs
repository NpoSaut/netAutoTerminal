using System;
using System.Collections.Generic;

namespace FirmwareBurner.BurningTools.AvrDude.Parameters
{
    /// <summary>Тип соединения с AVR ISP</summary>
    public enum AvrIspConnectionType
    {
        Usb
    }

    /// <summary>Параметр типа соединения с AVR ISP</summary>
    [ParameterKey('P')]
    public class ConnectionAvrDudeParameter : AvrDudeParameter
    {
        private readonly Dictionary<AvrIspConnectionType, string> _connectinTypePseudonames =
            new Dictionary<AvrIspConnectionType, string>
            {
                { AvrIspConnectionType.Usb, "usb" }
            };

        private readonly AvrIspConnectionType _connectionType;
        public ConnectionAvrDudeParameter(AvrIspConnectionType ConnectionType) { _connectionType = ConnectionType; }

        /// <summary>Значение параметра</summary>
        protected override string Value
        {
            get { return _connectinTypePseudonames[_connectionType]; }
        }

        public override string ToString() { return String.Format("Connection Type: {0}", _connectionType); }
    }
}
