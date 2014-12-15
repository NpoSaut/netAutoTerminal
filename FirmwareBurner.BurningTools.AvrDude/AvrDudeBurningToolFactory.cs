using ExternalTools.Implementations;
using ExternalTools.Interfaces;
using FirmwareBurner.BurningTools.AvrDude.AvrDudeBody;

namespace FirmwareBurner.BurningTools.AvrDude
{
    public class AvrDudeBurningToolFactory
    {
        private readonly IChipPseudonameProvider _chipPseudonameProvider;
        private readonly IToolBodyFactory _toolBodyFactory;
        private readonly IToolLauncher _toolLauncherFactory;

        public AvrDudeBurningToolFactory(IToolLauncher ToolLauncherFactoryFactory, IChipPseudonameProvider ChipPseudonameProvider)
        {
            _toolLauncherFactory = ToolLauncherFactoryFactory;
            _chipPseudonameProvider = ChipPseudonameProvider;
            _toolBodyFactory = new SingletonEmbeddedToolBodyFactoryBase(typeof (AvrDudeBodyMarker), "avrdude.exe");
        }

        public AvrDudeBurningTool GetBurningTool(string ChipName)
        {
            return new AvrDudeBurningTool(_chipPseudonameProvider.GetChipPseudoname(ChipName), _toolBodyFactory.GetToolBody(), _toolLauncherFactory);
        }
    }
}
