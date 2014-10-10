using System.IO;

namespace FirmwareBurner.Burning.Burners.AvrIsp
{
    public class AvrIspBurner : IFirmwareBurner
    {
        public AvrIspBurner(IAvrIspCommandShell Shell) { this.Shell = Shell; }
        private IAvrIspCommandShell Shell { get; set; }

        public void Burn(Pie pie, IBurningOperationStatusReceiver StatusReceiver)
        {
            Shell.ChipName = "AT90CAN128";

            var targetFuses =
                new Fuses
                {
                    FuseE = Pie.FuseE,
                    FuseH = Pie.FuseH,
                    FuseL = Pie.FuseL
                };

            //#if DEBUG
            pie.FlashFile.SaveTo("flash.hex");
            //#endif

            Fuses onDevFuses = Shell.ReadFuse();
            if (!onDevFuses.Equals(targetFuses))
                Shell.WriteFuse(targetFuses);

            var tempFlashFile = new FileInfo(Path.GetTempFileName());
            pie.FlashFile.SaveTo(tempFlashFile);
            Shell.WriteFlash(tempFlashFile);
            tempFlashFile.Delete();
        }
    }
}
