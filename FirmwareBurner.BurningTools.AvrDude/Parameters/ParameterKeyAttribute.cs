using System;

namespace FirmwareBurner.BurningTools.AvrDude.Parameters
{
    /// <summary>Имя параметра</summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ParameterKeyAttribute : Attribute
    {
        public ParameterKeyAttribute(char Name) { this.Name = Name; }
        public char Name { get; private set; }
    }
}
