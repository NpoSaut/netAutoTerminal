namespace FirmwareBurner.ImageFormatters.Avr
{
    public class AvrFuses
    {
        /// <summary>Инициализирует новый экземпляр класса <see cref="T:System.Object" />.</summary>
        public AvrFuses(byte FuseH, byte FuseL, byte FuseX)
        {
            this.FuseH = FuseH;
            this.FuseL = FuseL;
            this.FuseX = FuseX;
        }

        public byte FuseH { get; private set; }
        public byte FuseL { get; private set; }
        public byte FuseX { get; private set; }
    }
}
