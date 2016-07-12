using System.Diagnostics;
using System.IO;
using JetBrains.Annotations;
using Saut.AutoTerminal.Exceptions;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations.Cores
{
    /// <summary>Терминал, работающий поверх запущенного в системе процесса</summary>
    public class ProcessTerminalCore : IStreamTerminalCore
    {
        public ProcessTerminalCore([NotNull] Process CoreProcess)
        {
            Process coreProcess = CoreProcess;
            if (!IsConfiguredForInteraction(CoreProcess))
                throw new ProcessIsNotConfiguredForInteractionTerminalException(CoreProcess.ProcessName);

            Output = coreProcess.StandardOutput;
            Input = coreProcess.StandardInput;
        }

        public TextReader Output { get; private set; }
        public TextWriter Input { get; private set; }

        public void Dispose() { }

        private static bool IsConfiguredForInteraction(Process Process)
        {
            return !Process.StartInfo.UseShellExecute &&
                   Process.StartInfo.RedirectStandardInput &&
                   Process.StartInfo.RedirectStandardOutput;
        }
    }
}
