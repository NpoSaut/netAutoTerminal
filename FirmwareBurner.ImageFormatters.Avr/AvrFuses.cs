namespace FirmwareBurner.ImageFormatters.Avr
{
    /// <summary>Значения AVR FUSE-битов</summary>
    public class AvrFuses
    {
        /// <summary>Инициализирует новый экземпляр класса <see cref="T:System.Object" />.</summary>
        public AvrFuses(byte FuseH, byte FuseL, byte FuseX)
        {
            this.FuseH = FuseH;
            this.FuseL = FuseL;
            this.FuseX = FuseX;
        }

        /// <summary>Fuse High</summary>
        public byte FuseH { get; private set; }

        /// <summary>Fuse Low</summary>
        public byte FuseL { get; private set; }

        /// <summary>Extended Fuse</summary>
        public byte FuseX { get; private set; }
    }
}
