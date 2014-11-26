using System;
using FirmwareBurner.Annotations;
using FirmwareBurner.Burning;

namespace FirmwareBurner.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [BaseTypeRequired(typeof(IBurningToolFacadeFactory<>))]
    public class TargetDeviceAttribute : Attribute
    {
        public TargetDeviceAttribute(string DeviceName) { this.DeviceName = DeviceName; }
        public String DeviceName { get; private set; }
    }
}
