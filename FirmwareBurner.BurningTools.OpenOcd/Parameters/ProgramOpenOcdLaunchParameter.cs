using System.Collections.Generic;
using System.IO;

namespace FirmwareBurner.BurningTools.OpenOcd.Parameters
{
    /// <summary>Параметр запуска с командой на программирование ячейки</summary>
    [ParameterKind(OpenOcdParameterKind.Command)]
    internal class ProgramOpenOcdLaunchParameter : OpenOcdLaunchParameter
    {
        private readonly FileInfo _firmwareHex;
        private readonly bool _reset;
        private readonly bool _verify;

        public ProgramOpenOcdLaunchParameter(string FirmwareHexPath, bool Verify = true, bool Reset = true) : this(new FileInfo(FirmwareHexPath), Verify, Reset) { }

        public ProgramOpenOcdLaunchParameter(FileInfo FirmwareHex, bool Verify = true, bool Reset = true)
        {
            _firmwareHex = FirmwareHex;
            _verify = Verify;
            _reset = Reset;
        }

        private IEnumerable<string> EnumerateProgramActions()
        {
            yield return string.Format("program \"{0}\"", ProcessFilePath(_firmwareHex.FullName));
            if (_verify) yield return "verify";
            if (_reset) yield return "reset";
        }

        /// <summary>Возвращает содержимое параметра</summary>
        protected override string GetParameterContent()
        {
            return string.Join(" ", EnumerateProgramActions());
        }
    }
}
