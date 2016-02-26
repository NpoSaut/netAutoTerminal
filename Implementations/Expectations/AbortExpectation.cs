using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations.Expectations
{
    public class AbortExpectation : IExpectation
    {
        private readonly Func<Match, Exception> _exceptionFactory;

        public AbortExpectation([NotNull] string Pattern, [NotNull] Func<Match, Exception> ExceptionFactory)
            : this(new Regex(Pattern, RegexOptions.Multiline | RegexOptions.Compiled), ExceptionFactory) { }

        public AbortExpectation([NotNull] Regex Regex, [NotNull] Func<Match, Exception> ExceptionFactory)
        {
            this.Regex = Regex;
            _exceptionFactory = ExceptionFactory;
        }

        /// <summary>Требуемый метод поиска ожидания</summary>
        public SurfingMethod RequiredSurfingMethod
        {
            get { return SurfingMethod.ByCharacter; }
        }

        /// <summary>Искомое регулярное выражение</summary>
        public Regex Regex { get; private set; }

        /// <summary>Реакция на получение регулярного выражения</summary>
        /// <param name="Match">Результат поиска регулярного выражения</param>
        /// <returns>True, если требуется прекратить текущий этап ожидания</returns>
        public bool Activate(Match Match)
        {
            throw _exceptionFactory(Match);
        }
    }
}
