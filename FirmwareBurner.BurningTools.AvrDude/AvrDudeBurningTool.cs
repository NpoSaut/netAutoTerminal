using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ExternalTools.Interfaces;
using FirmwareBurner.BurningTools.AvrDude.Parameters;
using FirmwareBurner.Progress;

namespace FirmwareBurner.BurningTools.AvrDude
{
    public class AvrDudeBurningTool
    {
        private readonly String _chipPseudoname;
        private readonly IProgressControllerFactory _progressControllerFactory;
        private readonly IToolBody _toolBody;
        private readonly IToolLauncher _toolLauncher;

        public AvrDudeBurningTool(string ChipPseudoname, IToolBody ToolBody, IToolLauncher ToolLauncher, IProgressControllerFactory ProgressControllerFactory)
        {
            _toolBody = ToolBody;
            _toolLauncher = ToolLauncher;
            _progressControllerFactory = ProgressControllerFactory;
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
            var fuses = new Dictionary<AvrDudeMemoryType, byte>
                        {
                            { AvrDudeMemoryType.HFuse, f.FuseH },
                            { AvrDudeMemoryType.LFuse, f.FuseL },
                            { AvrDudeMemoryType.EFuse, f.FuseE }
                        };

            foreach (var fuse in fuses)
            {
                string res = LaunchAvrDude(fuse.Key, AvrDudeMemoryOperationType.Write, String.Format("0x{0:x2}", fuse.Value), AvrDudeInputFormat.Manual, null);
            }
        }

        private string LaunchAvrDude(AvrDudeMemoryType MemoryType, AvrDudeMemoryOperationType Operation, string Input, AvrDudeInputFormat InputFormat,
                                     IProgressToken ProgressToken)
        {
            Process p = _toolLauncher.Execute(_toolBody,
                                              new ConnectionAvrDudeParameter(AvrIspConnectionType.Usb),
                                              new ProgrammerIdAvrDudeParameter(ProgrammerType.AvrIsp),
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

            using (var progressManager = new CompositeProgressManager(ProgressToken, subtokens))
            {
                IEnumerator toktok = subtokens.GetEnumerator();
                IProgressController progressController = null;
                bool calculatingProgress = false;
                int counter = 0;
                Debug.WriteLine("-------------------------------------------");
                string bufferString = "";
                while (true)
                {
                    int x = p.StandardError.Read();
                    if (x != -1)
                    {
                        var c = (char)x;
                        Debug.Write(c);
                        bufferString += c;

                        if (c == '|')
                        {
                            if (!calculatingProgress)
                            {
                                toktok.MoveNext();
                                calculatingProgress = true;
                                counter = 0;
                                progressController = _progressControllerFactory.CreateController((IProgressToken)toktok.Current);
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
            return string.Empty;
        }

        private void POnErrorDataReceived(object Sender, DataReceivedEventArgs DataReceivedEventArgs) { Debug.Write("[{0}]", DataReceivedEventArgs.Data); }
    }
}
