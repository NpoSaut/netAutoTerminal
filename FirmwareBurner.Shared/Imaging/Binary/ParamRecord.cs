namespace FirmwareBurner.Imaging.Binary
{
    public class ParamRecord
    {
        public ParamRecord(byte Key, int Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

        public byte Key { get; private set; }
        public int Value { get; private set; }
    }
}
