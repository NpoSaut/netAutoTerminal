using System.Text.RegularExpressions;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations.Expectations
{
    /// <summary>Ожидание-барьер</summary>
    /// <remarks>Просто завершает процесс поиска после обнаружения искомой фразы</remarks>
    public class BarrierExpectation : IExpectation
    {
        public BarrierExpectation(string Pattern, SurfingMethod RequiredSurfingMethod = SurfingMethod.ByLine)
            : this(new Regex(Pattern, RegexOptions.Multiline | RegexOptions.Compiled), RequiredSurfingMethod) { }

        public BarrierExpectation(Regex Regex, SurfingMethod RequiredSurfingMethod = SurfingMethod.ByLine)
        {
            this.RequiredSurfingMethod = RequiredSurfingMethod;
            this.Regex = Regex;
        }

        public SurfingMethod RequiredSurfingMethod { get; private set; }
        public Regex Regex { get; private set; }
        public bool Activate(Match Match) { return true; }
    }
}
