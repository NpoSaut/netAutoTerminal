namespace FirmwareBurner.BurningTools.Stk500.Parameters
{
    internal class GetSignatureParameter : OneKeyParameter
    {
        public override string Key
        {
            get { return "s"; }
        }
    }
}