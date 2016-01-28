using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Saut.AutoTerminal.Implementations;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal
{
    public class TerminalManager
    {
        private readonly ITerminal _terminal;

        public TerminalManager(ITerminal Terminal) { _terminal = Terminal; }

        public void Run()
        {
            var rs = new RegexSurfer();

            var re = new ManualResetEventSlim(false);
            var t = new Task(() =>
                             {
                                 while (!re.IsSet)
                                 {
                                     _terminal.Input.WriteLine();
                                     Thread.Sleep(100);
                                 }
                             });

            rs.SeekForMatches(_terminal.Output,
                              new DelegateExpectation(@"U-Boot", true, Match => t.Start()));

            rs.SeekForMatches(_terminal.Output,
                              new DelegateExpectation(@"SMDKC100 #", true, Match =>
                                                                           {
                                                                               re.Set();
                                                                               _terminal.Input.WriteLine("nand erase 60000");
                                                                           }));

            var bads = new List<int>();

            rs.SeekForMatches(_terminal.Output,
                              new DelegateExpectation(@"Skipping bad block at  0x(?<bad>[0-9a-fA-F]+)\s*\n", false,
                                                      Match => bads.Add(int.Parse(Match.Groups["bad"].Value, NumberStyles.HexNumber))),
                              new DelegateExpectation("OK", Match => true));
            t.Wait();
        }
    }
}
