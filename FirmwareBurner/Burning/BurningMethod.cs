namespace FirmwareBurner.Burning
{
    public class BurningMethod : IBurningMethod
    {
        public BurningMethod(IBurningReceipt Receipt) { this.Receipt = Receipt; }

        public string Name
        {
            get { return Receipt.Name; }
        }

        public IBurningReceipt Receipt { get; private set; }
    }
}
