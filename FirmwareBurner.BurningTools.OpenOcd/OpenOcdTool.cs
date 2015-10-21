using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ExternalTools.Interfaces;
using FirmwareBurner.BurningTools.OpenOcd.Exceptions;
using FirmwareBurner.BurningTools.OpenOcd.Parameters;

namespace FirmwareBurner.BurningTools.OpenOcd
{
    public class OpenOcdTool
    {
        private readonly IToolLauncher _launcher;
        private readonly IToolBody _toolBody;

        public OpenOcdTool(IToolBody ToolBody, IToolLauncher Launcher)
        {
            _toolBody = ToolBody;
            _launcher = Launcher;
        }

        public void Burn(string BoardName, string TargetName, string FirmwareHexPath)
        {
            Process process = _launcher.Execute(_toolBody, GetLaunchParameters(BoardName, TargetName, FirmwareHexPath));
            //process.WaitForExit();
            var error = process.StandardError.ReadToEnd();
            var output = process.StandardOutput.ReadToEnd();
            if (process.ExitCode != 0)
                throw new OpenOcdException(process.ExitCode);
        }

        private IEnumerable<ILaunchParameter> GetLaunchParameters(string BoardName, string TargetName, string FirmwareHexPath)
        {
            yield return new ScriptsLocationOpenOcdLaunchParameter(Path.Combine(_toolBody.WorkingDirectoryPath, "scripts"));
            yield return new BoardConfigurationOpenOcdLaunchParameter(BoardName);
            if (TargetName != null)
                yield return new TargetOpenOcdLaunchParameter(TargetName);
            yield return new ProgramOpenOcdLaunchParameter(FirmwareHexPath);
            yield return new ShutdownOpenOcdLaunchParameter();
        }
    }
}
