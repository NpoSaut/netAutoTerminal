using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Saut.AutoTerminal.Exceptions;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations
{
    /// <summary>Инструмент для поиска регулярных выражений по выводу приложения</summary>
    public class RegexSurfer
    {
        /// <summary>Начинает поиск ожиданий в выводе приложения</summary>
        /// <param name="Terminal">Терминал для чтения</param>
        /// <param name="Expectations">Список ожиданий от приложения</param>
        /// <exception cref="NoMatchesFoundException">
        ///     Вывод приложения был прочитан до конца, но завершающее ожидание так и не
        ///     сработало
        /// </exception>
        public void SeekForMatches(ITerminal Terminal, IList<IExpectation> Expectations)
        {
            string buffer = string.Empty;
            try
            {
                while (true)
                {
                    buffer += Terminal.Read();
                    foreach (IExpectation expectation in Expectations)
                    {
                        Match match = expectation.Regex.Match(buffer);
                        if (match.Success)
                        {
                            buffer = string.Empty;
                            if (expectation.Activate(match))
                                return;
                        }
                    }
                }
            }
            catch (EndOfStreamTerminalException)
            {
                throw new NoMatchesFoundException(buffer, Expectations.Select(expectation => expectation.ToString()).ToList());
            }
        }
    }

    public static class RegexSurferHelper
    {
        /// <summary>Начинает поиск ожиданий в выводе приложения</summary>
        /// <param name="Surfer">Инструмент для поиска</param>
        /// <param name="Terminal">Терминал для чтения</param>
        /// <param name="Expectations">Список ожиданий от приложения</param>
        /// <exception cref="NoMatchesFoundException">
        ///     Вывод приложения был прочитан до конца, но завершающее ожидание так и не
        ///     сработало
        /// </exception>
        public static void SeekForMatches(this RegexSurfer Surfer, ITerminal Terminal, params IExpectation[] Expectations)
        {
            Surfer.SeekForMatches(Terminal, Expectations);
        }
    }
}
