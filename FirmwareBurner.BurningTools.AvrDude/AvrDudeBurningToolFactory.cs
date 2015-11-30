using AsyncOperations.Progress;
using ExternalTools.Interfaces;
using FirmwareBurner.BurningTools.AvrDude.Parameters;

namespace FirmwareBurner.BurningTools.AvrDude
{
    public class AvrDudeBurningToolFactory
    {
        private readonly IAvrDudeChipPseudonameProvider _chipPseudonameProvider;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IToolBodyFactory _toolBodyFactory;
        private readonly IToolLauncher _toolLauncher;

        public AvrDudeBurningToolFactory(IToolLauncher ToolLauncher, IAvrDudeChipPseudonameProvider ChipPseudonameProvider,
                                         IProgressControllerFactory ProgressControllerFactory, IToolBodyFactory ToolBodyFactory)
        {
            _toolLauncher = ToolLauncher;
            _chipPseudonameProvider = ChipPseudonameProvider;
            _progressControllerFactory = ProgressControllerFactory;
            _toolBodyFactory = ToolBodyFactory;
        }

        public AvrDudeBurningTool GetBurningTool(string ChipName, ProgrammerType ProgrammerKind)
        {
            return new AvrDudeBurningTool(_chipPseudonameProvider.GetChipPseudoname(ChipName), _toolBodyFactory.GetToolBody(), _toolLauncher,
                                          _progressControllerFactory, ProgrammerKind);
        }
    }
}
