namespace Saut.AutoTerminal
{
    public class TerminalManager
    {
        private readonly ITerminal _terminal;

        public TerminalManager(ITerminal Terminal) { _terminal = Terminal; }

        public void Run()
        {
            var rs = new RegexSeeker();
            rs.SeekForMatches(_terminal.Output,
                new DelegateExpectation(@"Hit any key to stop autoboot:  \d", Match => _terminal.Input.WriteLine()));
        }
    }
}
