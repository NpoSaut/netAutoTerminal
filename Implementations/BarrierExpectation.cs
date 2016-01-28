using System.Text.RegularExpressions;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations
{
    public class BarrierExpectation : IExpectation
    {
        public BarrierExpectation(string Pattern) : this(new Regex(Pattern)) { }
        public BarrierExpectation(Regex Regex) { this.Regex = Regex; }
        public Regex Regex { get; private set; }
        public bool Activate(Match Match) { return true; }
    }
}
