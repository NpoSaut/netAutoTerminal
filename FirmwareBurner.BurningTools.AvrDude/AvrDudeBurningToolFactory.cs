using AsyncOperations.Progress;
using ExternalTools.Implementations;
using ExternalTools.Interfaces;
using FirmwareBurner.BurningTools.AvrDude.AvrDudeBody;
using FirmwareBurner.BurningTools.AvrDude.Parameters;

namespace FirmwareBurner.BurningTools.AvrDude
{
    public class AvrDudeBurningToolFactory
    {
        private readonly IChipPseudonameProvider _chipPseudonameProvider;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IToolBodyFactory _toolBodyFactory;
        private readonly IToolLauncher _toolLauncher;

        public AvrDudeBurningToolFactory(IToolLauncher ToolLauncher, IChipPseudonameProvider ChipPseudonameProvider,
                                         IProgressControllerFactory ProgressControllerFactory)
        {
            _toolLauncher = ToolLauncher;
            _chipPseudonameProvider = ChipPseudonameProvider;
            _progressControllerFactory = ProgressControllerFactory;
            _toolBodyFactory = new SingletonEmbeddedToolBodyFactoryBase(typeof (AvrDudeBodyMarker), "avrdude.exe");
        }

        public AvrDudeBurningTool GetBurningTool(string ChipName, ProgrammerType ProgrammerKind)
        {
            return new AvrDudeBurningTool(_chipPseudonameProvider.GetChipPseudoname(ChipName), _toolBodyFactory.GetToolBody(), _toolLauncher,
                                          _progressControllerFactory, ProgrammerKind);
        }
    }
}
