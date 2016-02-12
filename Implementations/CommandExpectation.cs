using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations
{
    /// <summary>Ожидание с вводом команды</summary>
    /// <remarks>Вводит команду после получения ожидаемого текста</remarks>
    public class CommandExpectation : IExpectation
    {
        private readonly string _command;
        private readonly object[] _parameters;
        private readonly ITerminal _terminal;

        [StringFormatMethod("Command")]
        public CommandExpectation(string Pattern, ITerminal Terminal, string Command, params object[] Parameters)
            : this(new Regex(Pattern, RegexOptions.Compiled | RegexOptions.Multiline), Terminal, Command, Parameters) { }

        [StringFormatMethod("Command")]
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
            _terminal.WriteLine(_command, _parameters);
            return true;
        }
    }
}
