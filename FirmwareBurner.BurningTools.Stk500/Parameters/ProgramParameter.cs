namespace FirmwareBurner.BurningTools.Stk500.Parameters
{
    internal class ProgramParameter : Stk500Parameter
    {
        public enum ProgramTarget
        {
            flash,
            eeprom,
            both
        }

        public ProgramParameter(ProgramTarget target) { this.target = target; }
        public ProgramTarget target { get; set; }
        protected override string Combine() { return string.Format("p{0}", target.ToString()[0]); }
    }
}