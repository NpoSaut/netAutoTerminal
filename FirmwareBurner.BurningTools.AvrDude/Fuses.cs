namespace FirmwareBurner.BurningTools.AvrDude
{
    /// <summary>Fuse-биты</summary>
    public class Fuses
    {
        /// <summary>Создаёт экземпляр Fuse-битов</summary>
        public Fuses(byte FuseH, byte FuseL, byte FuseE)
        {
            this.FuseH = FuseH;
            this.FuseL = FuseL;
            this.FuseE = FuseE;
        }

        public byte FuseH { get; private set; }
        public byte FuseL { get; private set; }
        public byte FuseE { get; private set; }
    }
}
