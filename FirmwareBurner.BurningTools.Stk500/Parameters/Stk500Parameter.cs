namespace FirmwareBurner.BurningTools.Stk500.Parameters
{
    internal abstract class Stk500Parameter
    {
        protected abstract string Combine();
        public string Get() { return string.Format("-{0}", Combine()); }
    }
}
