using System.IO;
using ExternalTools.Implementations;
using ExternalTools.Interfaces;

namespace FirmwareBurner.BurningTools.OpenOcd
{
    public class OpenOcdToolFactory
    {
        private readonly IToolBodyFactory _toolBodyFactory;
        private readonly IToolLauncher _toolLauncher;

        public OpenOcdToolFactory(IToolLauncher ToolLauncher)
        {
            _toolLauncher = ToolLauncher;
            _toolBodyFactory = new StaticToolBodyFactory(Path.Combine("Tools", "OpenOCD"), Path.Combine("bin", "openocd.exe"));
        }

        public OpenOcdTool GetBurningTool() { return new OpenOcdTool(_toolBodyFactory.GetToolBody(), _toolLauncher); }
    }
}
