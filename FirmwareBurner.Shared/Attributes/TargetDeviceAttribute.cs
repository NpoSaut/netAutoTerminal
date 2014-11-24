using System;
using FirmwareBurner.Annotations;
using FirmwareBurner.Burning;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [MeansImplicitUse]
    public abstract class TargetDeviceAttribute : Attribute
    {
        protected TargetDeviceAttribute(string Name) { this.Name = Name; }
        public String Name { get; private set; }
    }

    [BaseTypeRequired(typeof (IBurningToolFacadeFactory<>))]
    public class BurnsDeviceAttribute : TargetDeviceAttribute
    {
        public BurnsDeviceAttribute(string Name) : base(Name) { }
    }

    [BaseTypeRequired(typeof (IImageFormatter<>))]
    public class ImagesForDeviceAttribute : TargetDeviceAttribute
    {
        public ImagesForDeviceAttribute(string Name) : base(Name) { }
    }
}
