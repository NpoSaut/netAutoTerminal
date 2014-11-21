namespace FirmwareBurner.BurningTools.Stk500.Parameters
{
    internal abstract class OneKeyParameter : Stk500Parameter
    {
        public abstract string Key { get; }
        protected override string Combine() { return Key; }
    }
}