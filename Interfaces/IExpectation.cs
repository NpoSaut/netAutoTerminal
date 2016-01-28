using System.Text.RegularExpressions;

namespace Saut.AutoTerminal.Interfaces
{
    public interface IExpectation
    {
        Regex Regex { get; }
        bool Activate(Match Match);
    }
}
