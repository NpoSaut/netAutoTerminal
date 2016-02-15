using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Saut.AutoTerminal.Exceptions;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations
{
    /// <summary>Метод чтения вывода терминала</summary>
    public enum SurfingMethod
    {
        /// <summary>По символу</summary>
        ByCharacter = 0,

        /// <summary>По строчке</summary>
        ByLine = 1
    }

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
            var surfingMethod = Expectations.Min(e => e.RequiredSurfingMethod);
            var buffer = new StringBuilder();
            try
            {
                while (true)
                {
                    switch (surfingMethod)
                    {
                        case SurfingMethod.ByCharacter:
                            buffer.Append(Terminal.Read());
                            break;
                        case SurfingMethod.ByLine:
                            buffer.AppendLine(Terminal.ReadLine());
                            break;
                    }

                    foreach (IExpectation expectation in Expectations)
                    {
                        Match match = expectation.Regex.Match(buffer.ToString());
                        if (match.Success)
                        {
                            if (expectation.Activate(match))
                                return;
                            buffer.Clear();
                        }
                    }
                }
            }
            catch (EndOfStreamTerminalException)
            {
                throw new NoMatchesFoundException(buffer.ToString(), Expectations.Select(expectation => expectation.ToString()).ToList());
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
