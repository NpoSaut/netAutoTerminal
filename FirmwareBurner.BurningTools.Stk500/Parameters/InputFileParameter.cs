using System.IO;

namespace FirmwareBurner.BurningTools.Stk500.Parameters
{
    internal class InputFileParameter : Stk500Parameter
    {
        public enum FilePlacement
        {
            flash,
            eeprom
        }

        public InputFileParameter(FileInfo File, FilePlacement placement)
        {
            this.File = File;
            this.placement = placement;
        }

        public FilePlacement placement { get; set; }
        public FileInfo File { get; private set; }

        protected override string Combine() { return string.Format("i{0}\"{1}\"", placement.ToString()[0], File.FullName); }
    }
}