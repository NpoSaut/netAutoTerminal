using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
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

        public void InitializeProgrammer()
        {
            Process process = _launcher.Execute(_toolBody,
                                                new ScriptsLocationOpenOcdLaunchParameter(Path.Combine(_toolBody.WorkingDirectoryPath, "scripts")),
                                                new ProgrammerConfigurationOpenOcdLaunchParameter("ftdi/olimex-arm-usb-tiny-h.cfg"),
                                                new ShutdownOpenOcdLaunchParameter());
            string output = process.StandardError.ReadToEnd();
        }

        public void Burn(string BoardName, string TargetName, string FirmwareHexPath)
        {
            Process process = _launcher.Execute(_toolBody, GetLaunchParameters(BoardName, TargetName, FirmwareHexPath));
            string output = process.StandardError.ReadToEnd();

            CheckOutput(output, "Error: no device found",
                        g => new OpenOcdProgrammerNotConnectedException(output));
            CheckOutput(output, "Error: JTAG scan chain interrogation failed",
                        g => new OpenOcdDeviceNotConnectedException(output));
            CheckOutput(output, @"Runtime Error: embedded:startup\.tcl:\d+: Can't find /board/(?<BoardName>.+)\.cfg",
                        g => new OpenOcdConfigurationFileNotFoundException(g["BoardName"].Value, output));

            if (!output.Contains("** Verified OK **"))
                throw new UnknownOpenOcdException(output);

            if (process.ExitCode != 0)
                throw new OpenOcdException(process.ExitCode, output);
        }

        private void CheckOutput(string Output, string Expression, Func<GroupCollection, OpenOcdException> ExceptionFactory)
        {
            var r = new Regex(Expression);
            Match m = r.Match(Output);
            if (m.Success)
                throw ExceptionFactory(m.Groups);
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
