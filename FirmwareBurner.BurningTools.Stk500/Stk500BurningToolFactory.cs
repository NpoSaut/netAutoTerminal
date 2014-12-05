using FirmwareBurner.BurningTools.Stk500.Launching;

namespace FirmwareBurner.BurningTools.Stk500
{
    public class Stk500BurningToolFactory
    {
        private readonly IToolBodyFactory _toolBodyFactory;
        private readonly IToolLauncher _toolLauncher;

        public Stk500BurningToolFactory(IToolBodyFactory ToolBodyFactory, IToolLauncher ToolLauncher)
        {
            _toolBodyFactory = ToolBodyFactory;
            _toolLauncher = ToolLauncher;
        }

        public Stk500BurningTool GetBurningTool(string ChipName) { return new Stk500BurningTool(ChipName, _toolBodyFactory.GetToolBody(), _toolLauncher); }
    }
}
