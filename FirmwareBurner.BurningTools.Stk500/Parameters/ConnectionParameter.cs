namespace FirmwareBurner.BurningTools.Stk500.Parameters
{
    internal class ConnectionParameter : Stk500Parameter
    {
        public enum ConnectionType
        {
            USB
        }

        public ConnectionParameter(ConnectionType connection = ConnectionType.USB) { this.connection = connection; }
        public ConnectionType connection { get; set; }
        protected override string Combine() { return string.Format("c{0}", connection); }
    }
}