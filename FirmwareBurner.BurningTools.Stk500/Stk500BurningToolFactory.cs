using ExternalTools.Implementations;
using ExternalTools.Interfaces;
using FirmwareBurner.Annotations;
using FirmwareBurner.BurningTools.Stk500.Stk500Body;

namespace FirmwareBurner.BurningTools.Stk500
{
    [UsedImplicitly]
    public class Stk500BurningToolFactory
    {
        private readonly IToolBodyFactory _toolBodyFactory;
        private readonly IToolLauncher _toolLauncher;

        public Stk500BurningToolFactory(IToolLauncher ToolLauncher)
        {
            _toolLauncher = ToolLauncher;
            _toolBodyFactory = new SingletonEmbeddedToolBodyFactoryBase(typeof (Stk500BodyMarker), "Stk500.exe");
        }

        public Stk500BurningTool GetBurningTool(string ChipName) { return new Stk500BurningTool(ChipName, _toolBodyFactory.GetToolBody(), _toolLauncher); }
    }
}
