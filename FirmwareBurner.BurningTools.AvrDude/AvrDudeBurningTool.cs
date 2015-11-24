using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using AsyncOperations.Progress;
using ExternalTools.Interfaces;
using FirmwareBurner.BurningTools.AvrDude.Exceptions;
using FirmwareBurner.BurningTools.AvrDude.Parameters;

namespace FirmwareBurner.BurningTools.AvrDude
{
    public class AvrDudeBurningTool
    {
        private readonly String _chipPseudoname;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IToolBody _toolBody;
        private readonly IToolLauncher _toolLauncher;
        private readonly ProgrammerType _programmerKind;

        public AvrDudeBurningTool(string ChipPseudoname, IToolBody ToolBody, IToolLauncher ToolLauncher, IProgressControllerFactory ProgressControllerFactory, ProgrammerType ProgrammerKind)
        {
            _toolBody = ToolBody;
            _toolLauncher = ToolLauncher;
            _progressControllerFactory = ProgressControllerFactory;
            _programmerKind = ProgrammerKind;
            _chipPseudoname = ChipPseudoname;
        }

        public void WriteFlash(FileInfo FlashFile, IProgressToken ProgressToken = null)
        {
            LaunchAvrDude(AvrDudeMemoryType.Flash, AvrDudeMemoryOperationType.Write, FlashFile.FullName, AvrDudeInputFormat.IntelHex, ProgressToken);
        }

        public void WriteEeprom(FileInfo EepromFile, IProgressToken ProgressToken = null)
        {
            LaunchAvrDude(AvrDudeMemoryType.Eeprom, AvrDudeMemoryOperationType.Write, EepromFile.FullName, AvrDudeInputFormat.IntelHex, ProgressToken);
        }

        public Fuses ReadFuse() { throw new NotImplementedException(); }

        public void WriteFuse(Fuses f, IProgressToken ProgressToken)
        {
            var fuses = new[]
                        {
                            new { MemType = AvrDudeMemoryType.HFuse, Data = f.FuseH, ProgressToken = new SubprocessProgressToken() },
                            new { MemType = AvrDudeMemoryType.LFuse, Data = f.FuseL, ProgressToken = new SubprocessProgressToken() },
                            new { MemType = AvrDudeMemoryType.EFuse, Data = f.FuseE, ProgressToken = new SubprocessProgressToken() }
                        };

            foreach (var fuse in fuses)
            {
                string res = LaunchAvrDude(fuse.MemType, AvrDudeMemoryOperationType.Write, String.Format("0x{0:x2}", fuse.Data), AvrDudeInputFormat.Manual,
                                           ProgressToken);
            }
        }

        private string LaunchAvrDude(AvrDudeMemoryType MemoryType, AvrDudeMemoryOperationType Operation, string Input, AvrDudeInputFormat InputFormat,
                                     IProgressToken ProgressToken)
        {
            Process p = _toolLauncher.Execute(_toolBody,
                                              new ConnectionAvrDudeParameter(AvrIspConnectionType.Usb),
                                              new ProgrammerIdAvrDudeParameter(_programmerKind),
                                              new ChipIdAvrDudeParameter(_chipPseudoname),
                                              new UCommandParameter(MemoryType,
                                                                    Operation,
                                                                    Input,
                                                                    InputFormat));

            var subtokens = new[]
                            {
                                new SubprocessProgressToken(0.1),
                                new SubprocessProgressToken(1.0),
                                new SubprocessProgressToken(2.0)
                            };

            string output = string.Empty;
            using (new CompositeProgressManager(ProgressToken, subtokens))
            {
                IEnumerator tokensEnumerator = subtokens.GetEnumerator();
                IProgressController progressController = null;
                bool calculatingProgress = false;
                int counter = 0;

                while (true)
                {
                    int x = p.StandardError.Read();
                    if (x != -1)
                    {
                        var c = (char)x;
                        output += c;
                        Debug.Write(c);

                        if (c == '|')
                        {
                            if (!calculatingProgress)
                            {
                                tokensEnumerator.MoveNext();
                                calculatingProgress = true;
                                counter = 0;
                                progressController = _progressControllerFactory.CreateController((IProgressToken)tokensEnumerator.Current);
                            }
                            else
                            {
                                calculatingProgress = false;
                                progressController.Dispose();
                            }
                        }
                        if (c == '#' && calculatingProgress)
                        {
                            counter++;
                            progressController.SetProgress(counter / 50.0);
                        }
                    }
                    else if (p.HasExited)
                        break;
                }
            }
            p.WaitForExit();

            if (output.Contains("did not find any USB device \"usb\""))
                throw new ProgrammerNotConnectedAvrDudeException(output);

            if (output.Contains("initialization failed"))
                throw new DeviceNotConnectedAvrDudeException(output);

            if (!output.Contains("OK"))
                throw new AvrDudeException("Неизвестная ошибка при работе с AVRDude", output);

            return string.Empty;
        }
    }
}
