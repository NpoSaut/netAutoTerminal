using ExternalTools.Interfaces;

namespace FirmwareBurner.BurningTools.Stk500.Parameters
{
    public abstract class Stk500Parameter : ILaunchParameter
    {
        public string GetStringPresentation() { return string.Format("-{0}", Combine()); }
        protected abstract string Combine();
    }
}
