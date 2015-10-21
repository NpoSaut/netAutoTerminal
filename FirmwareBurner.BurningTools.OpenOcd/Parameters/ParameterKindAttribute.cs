using System;

namespace FirmwareBurner.BurningTools.OpenOcd.Parameters
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class ParameterKindAttribute : Attribute
    {
        public ParameterKindAttribute(OpenOcdParameterKind ParameterKind) { this.ParameterKind = ParameterKind; }
        public OpenOcdParameterKind ParameterKind { get; private set; }
    }
}
