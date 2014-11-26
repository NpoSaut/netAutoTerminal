using System;
using FirmwareBurner.Annotations;
using FirmwareBurner.Burning;

namespace FirmwareBurner.Attributes
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [BaseTypeRequired(typeof (IBurningToolFacadeFactory<>))]
    public class BurningReceiptFactoryAttribute : Attribute
    {
        public BurningReceiptFactoryAttribute(string Name) { this.Name = Name; }
        public String Name { get; private set; }
    }
}
