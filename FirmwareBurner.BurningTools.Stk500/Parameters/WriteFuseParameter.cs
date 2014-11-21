using System;

namespace FirmwareBurner.BurningTools.Stk500.Parameters
{
    internal class WriteFuseParameter : Stk500Parameter
    {
        public WriteFuseParameter(byte High, byte Low)
        {
            this.High = High;
            this.Low = Low;
        }

        public Byte High { get; set; }
        public Byte Low { get; set; }

        protected override string Combine() { return string.Format("f{0:X2}{1:X2}", High, Low); }
    }
}