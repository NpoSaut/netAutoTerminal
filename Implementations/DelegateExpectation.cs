using System;
using System.Text.RegularExpressions;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations
{
    public class DelegateExpectation : IExpectation
    {
        private readonly Func<Match, bool> _activationDelegate;

        public DelegateExpectation(String Pattern, bool FinalizesSequence, Action<Match> ActivationDelegate)
            : this(Pattern, DefaultDelegate(ActivationDelegate, FinalizesSequence)) { }

        public DelegateExpectation(String Pattern, Func<Match, bool> ActivationDelegate)
            : this(new Regex(Pattern, RegexOptions.Multiline | RegexOptions.Compiled), ActivationDelegate) { }

        public DelegateExpectation(Regex Regex, bool FinalizesSequence, Action<Match> ActivationDelegate)
            : this(Regex, DefaultDelegate(ActivationDelegate, FinalizesSequence)) { }

        public DelegateExpectation(Regex Regex, Func<Match, bool> ActivationDelegate)
        {
            this.Regex = Regex;
            _activationDelegate = ActivationDelegate;
        }

        public Regex Regex { get; private set; }
        public bool Activate(Match Match) { return _activationDelegate(Match); }

        private static Func<Match, bool> DefaultDelegate(Action<Match> ActivationDelegate, bool FinalizesSequence)
        {
            return Match =>
                   {
                       ActivationDelegate(Match);
                       return FinalizesSequence;
                   };
        }
    }
}
