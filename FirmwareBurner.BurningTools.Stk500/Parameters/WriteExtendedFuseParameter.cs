using System;

namespace FirmwareBurner.BurningTools.Stk500.Parameters
{
    internal class WriteExtendedFuseParameter : Stk500Parameter
    {
        public WriteExtendedFuseParameter(byte FuseE) { Value = FuseE; }
        public Byte Value { get; set; }

        protected override string Combine() { return string.Format("E{0:X2}", Value); }
    }
}