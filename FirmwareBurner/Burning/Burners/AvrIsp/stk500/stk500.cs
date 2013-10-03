using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using FirmwareBurner.Burning.Burners.AvrIsp;
using System.Text.RegularExpressions;

namespace FirmwareBurner.Burning.Burners.AvrIsp.stk500
{
    /// <summary>
    /// Оболочка над программой STK500.exe
    /// </summary>
    public class Stk500 : IAvrIspCommandShell
    {
        private static readonly FileInfo _BurnerFile = new FileInfo(Path.Combine("stk500", "stk500.exe"));
        public static FileInfo BurnerFile { get { return _BurnerFile; } }

        public String ChipName { get; set; }

        public Stk500()
        {
            // Проверяем, доступна ли программа-прошивщик
            if (!BurnerFile.Exists) throw new BurnerNotFoundException();
        }

        public Byte[] GetSignature()
        {
            var output = Execute(
                new List<Stk500Parameter>()
                {
                    new ConnectionParameter(),
                    new DeviceNameParameter(ChipName),
                    new GetSignatureParameter()
                }).ReadToEnd();
            CheckOutputForErrors(output);

            Regex r = new Regex(@"Signature is (0x(?<byte>[0-9a-fA-F]{2})\s+){3}");
            var m = r.Match(output);
            if (m.Success)
                return m.Groups["byte"].Captures.OfType<Capture>().Select(bc => Convert.ToByte(bc.Value, 16)).ToArray();
            else
                throw new Stk500Exception("Программатор ответил не стандартным образом:\n\n" + output);
        }

        public void WriteFlash(FileInfo FlashFile, bool Erase = true)
        {
            var output = Execute(
                new List<Stk500Parameter>()
                {
                    new ConnectionParameter(),
                    new DeviceNameParameter(ChipName),
                    new ProgramParameter(ProgramParameter.ProgramTarget.flash),
                    new InputFileParameter(FlashFile, InputFileParameter.FilePlacement.flash),
                    Erase ? new EraseParameter() : null,
                });
            var OutputString = output.ReadToEnd();
            CheckOutputForErrors(OutputString);
        }

        public void WriteEeprom(FileInfo EepromFile, bool Erase = true)
        {
            throw new NotImplementedException();
        }

        public Fuses ReadFuse()
        {
            throw new NotImplementedException();
        }

        public void WriteFuse(Fuses f)
        {
            throw new NotImplementedException();
        }

        private void CheckOutputForErrors(string output)
        {
            if (output.Contains("Could not connect to AVRISP mkII"))
                throw new ProgrammerIsNotConnectedException();

            if (output.Contains("Could not enter programming mode"))
                throw new DeviceIsNotConnectedException();
        }

        private StreamReader Execute(IEnumerable<Stk500Parameter> Parameters)
        {
            var psi =
                new ProcessStartInfo(BurnerFile.FullName, string.Join(" ", Parameters.Where(prm => prm != null).Select(prm => prm.Get())))
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
            Process p = 
                new Process()
                {
                    StartInfo = psi
                };
            p.Start();
            return p.StandardOutput;
        }
    }
}
