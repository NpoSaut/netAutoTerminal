using System;

namespace FirmwareBurner.BurningTools.Stk500.Parameters
{
    internal class DeviceNameParameter : Stk500Parameter
    {
        public DeviceNameParameter(String DeviceName) { this.DeviceName = DeviceName; }
        public string DeviceName { get; set; }
        protected override string Combine() { return string.Format("d{0}", DeviceName); }
    }
}