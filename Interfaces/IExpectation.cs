using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Saut.AutoTerminal.Implementations;

namespace Saut.AutoTerminal.Interfaces
{
    /// <summary>Ожидание от терминала</summary>
    public interface IExpectation
    {
        /// <summary>Требуемый метод поиска ожидания</summary>
        SurfingMethod RequiredSurfingMethod { get; }

        /// <summary>Искомое регулярное выражение</summary>
        Regex Regex { get; }

        /// <summary>Реакция на получение регулярного выражения</summary>
        /// <param name="Match">Результат поиска регулярного выражения</param>
        /// <returns>True, если требуется прекратить текущий этап ожидания</returns>
        bool Activate([NotNull] Match Match);
    }
}
