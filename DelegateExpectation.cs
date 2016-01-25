using System;
using System.Text.RegularExpressions;

namespace Saut.AutoTerminal
{
    public class DelegateExpectation : IExpectation
    {
        private readonly Action<Match> _activateAction;

        public DelegateExpectation(String Pattern, Action<Match> ActivateAction)
            : this(new Regex(Pattern, RegexOptions.Multiline), ActivateAction) { }

        public DelegateExpectation(Regex Regex, Action<Match> ActivateAction)
        {
            this.Regex = Regex;
            _activateAction = ActivateAction;
        }

        public Regex Regex { get; private set; }
        public void Activate(Match Match) { _activateAction(Match); }
    }
}
