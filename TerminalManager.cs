using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Saut.AutoTerminal.Implementations;
using Saut.AutoTerminal.Implementations.Expectations;
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
                                     _terminal.WriteLine();
                                     Thread.Sleep(100);
                                 }
                             });

            rs.SeekForMatches(_terminal,
                              new DelegateExpectation(@"U-Boot", true, Match => t.Start()));

            rs.SeekForMatches(_terminal,
                              new DelegateExpectation(@"SMDKC100 #", true, Match =>
                                                                           {
                                                                               re.Set();
                                                                               _terminal.WriteLine("nand erase 60000");
                                                                           }));

            var bads = new List<int>();

            rs.SeekForMatches(_terminal,
                              new DelegateExpectation("OK", Match => true));
            t.Wait();
        }
    }
}
