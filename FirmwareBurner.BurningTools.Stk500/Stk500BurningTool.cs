using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ExternalTools.Interfaces;
using FirmwareBurner.Burning.Exceptions;
using FirmwareBurner.BurningTools.Stk500.Exceptions;
using FirmwareBurner.BurningTools.Stk500.Parameters;

namespace FirmwareBurner.BurningTools.Stk500
{
    /// <summary>Оболочка над программой STK500.exe</summary>
    public class Stk500BurningTool : IAvrIspCommandShell
    {
        private readonly String _chipName;
        private readonly IToolBody _toolBody;
        private readonly IToolLauncher _toolLauncher;

        public Stk500BurningTool(string ChipName, IToolBody ToolBody, IToolLauncher ToolLauncher)
        {
            _chipName = ChipName;
            _toolBody = ToolBody;
            _toolLauncher = ToolLauncher;
        }

        public Byte[] GetSignature()
        {
            string output = _toolLauncher.Execute(
                _toolBody,
                new ConnectionParameter(),
                new DeviceNameParameter(_chipName),
                new GetSignatureParameter()).ReadToEnd();
            CheckOutputForErrors(output);

            var r = new Regex(@"Signature is (0x(?<byte>[0-9a-fA-F]{2})\s+){3}");
            Match m = r.Match(output);
            if (m.Success)
                return m.Groups["byte"].Captures.OfType<Capture>().Select(bc => Convert.ToByte(bc.Value, 16)).ToArray();
            throw new Stk500Exception("Программатор ответил не стандартным образом:\n\n" + output);
        }

        public void WriteFlash(FileInfo FlashFile, bool Erase = true)
        {
            string output = _toolLauncher.Execute(
                _toolBody,
                new ConnectionParameter(),
                new DeviceNameParameter(_chipName),
                new ProgramParameter(ProgramParameter.ProgramTarget.flash),
                new InputFileParameter(FlashFile, InputFileParameter.FilePlacement.flash),
                Erase ? new EraseParameter() : null).ReadToEnd();
            CheckOutputForErrors(output, "FLASH programmed");
        }

        public void WriteEeprom(FileInfo EepromFile, bool Erase = true) { throw new NotImplementedException(); }

        public Fuses ReadFuse()
        {
            string output = _toolLauncher.Execute(
                _toolBody,
                new ConnectionParameter(),
                new DeviceNameParameter(_chipName),
                new ReadFuseParameter()).ReadToEnd();
            CheckOutputForErrors(output, "Fuse byte 0 read");

            var r = new Regex(@"Fuse byte (?<key>[012]) read \(0x(?<byte>[0-9a-fA-F]{2})\)");

            var res = new Fuses();
            foreach (Match m in r.Matches(output).OfType<Match>())
            {
                int i = int.Parse(m.Groups["key"].Value);
                byte val = Convert.ToByte(m.Groups["byte"].Value, 16);

                switch (i)
                {
                    case 0:
                        res.FuseL = val;
                        break;
                    case 1:
                        res.FuseH = val;
                        break;
                    case 2:
                        res.FuseE = val;
                        break;
                }
            }
            return res;
        }

        public void WriteFuse(Fuses f)
        {
            string output = _toolLauncher.Execute(
                _toolBody,
                new ConnectionParameter(),
                new DeviceNameParameter(_chipName),
                new WriteFuseParameter(f.FuseH, f.FuseL),
                new WriteExtendedFuseParameter(f.FuseE)).ReadToEnd();
            CheckOutputForErrors(output, "Fuse bits programmed");
        }

        private void CheckOutputForErrors(string Output, string SuccessString)
        {
            CheckOutputForErrors(Output);
            if (!Output.Contains(SuccessString))
                throw new BurningException(Output);
        }

        private void CheckOutputForErrors(string Output)
        {
            if (Output.Contains("Could not connect to AVRISP mkII"))
                throw new ProgrammerIsNotConnectedException();

            if (Output.Contains("Could not enter programming mode"))
                throw new DeviceIsNotConnectedException();
        }
    }
}
