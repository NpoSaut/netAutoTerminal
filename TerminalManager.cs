using System.Threading;
using System.Threading.Tasks;

namespace Saut.AutoTerminal
{
    public class TerminalManager
    {
        private readonly ITerminal _terminal;

        public TerminalManager(ITerminal Terminal) { _terminal = Terminal; }

        public void Run()
        {
            var rs = new RegexSeeker();

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
                              new DelegateExpectation(@"U-Boot", Match => t.Start()));

            rs.SeekForMatches(_terminal.Output,
                              new DelegateExpectation(@"SMDKC100 #", Match => re.Set()));
            t.Wait();
        }
    }
}
