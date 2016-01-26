using System.Text.RegularExpressions;

namespace Saut.AutoTerminal
{
    public interface IExpectation
    {
        Regex Regex { get; }
        bool Activate(Match Match);
    }
}
