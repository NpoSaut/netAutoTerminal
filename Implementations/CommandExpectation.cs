using System.Text.RegularExpressions;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations
{
    public class CommandExpectation : IExpectation
    {
        private readonly string _command;
        private readonly object[] _parameters;
        private readonly ITerminal _terminal;

        public CommandExpectation(string Pattern, ITerminal Terminal, string Command, params object[] Parameters)
            : this(new Regex(Pattern, RegexOptions.Compiled | RegexOptions.Multiline), Terminal, Command, Parameters) { }

        public CommandExpectation(Regex Regex, ITerminal Terminal, string Command, params object[] Parameters)
        {
            this.Regex = Regex;
            _command = Command;
            _parameters = Parameters;
            _terminal = Terminal;
        }

        public Regex Regex { get; private set; }

        public bool Activate(Match Match)
        {
            _terminal.Input.WriteLine(_command, _parameters);
            return true;
        }
    }
}
