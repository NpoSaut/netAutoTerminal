using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwareBurner.Burning;
using FirmwareBurner.Formating;
using FirmwarePacking;

namespace FirmwareBurner.Models
{
    public class BurningViewModel : ViewModelBase
    {
        private IFirmwareBurner Burner { get; set; }
        private IFirmwareFormatter Formatter { get; set; }
        private IFirmwareCook Coock { get; set; }

        public FirmwarePackage Firmware { get; set; }
        public ComponentTarget Target { get; set; }
        public BlockDetailsViewModel BlockDetails { get; set; }

        public ActionCommand BurnCommand { get; private set; }

        private BurningViewModel()
        {
            BurnCommand = new ActionCommand(Burn, CanBurn);
        }
        public BurningViewModel(IFirmwareBurner WithBurner, IFirmwareFormatter WithFormatter, IFirmwareCook Coock)
            : this()
        {
            this.Burner = WithBurner;
            this.Formatter = WithFormatter;
            this.Coock = Coock;
        }

        private bool CanBurn()
        {
            return
                Firmware != null &&
                Target != null &&
                BlockDetails != null &&
                BlockDetails.SerialNumber > 0 &&
                BlockDetails.AssemblyDate <= DateTime.Now &&
                Firmware.Components.Any(c => c.Targets.Contains(Target));
        }
        private void Burn()
        {
            Pie p = Coock.Cook(Firmware, Target, BlockDetails.SerialNumber, BlockDetails.AssemblyDate);
            Burner.Burn(p);
        }
    }
}
