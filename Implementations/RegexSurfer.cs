using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Saut.AutoTerminal.Exception;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations
{
    /// <summary>Инструмент для поиска регулярных выражений по выводу приложения</summary>
    public class RegexSurfer
    {
        /// <summary>Начинает поиск ожиданий в выводе приложения</summary>
        /// <param name="Reader"><see cref="TextReader" /> для чтения из потока вывода приложения</param>
        /// <param name="Expectations">Список ожиданий от приложения</param>
        /// <exception cref="NoMatchesFoundException">
        ///     Вывод приложения был прочитан до конца, но завершающее ожидание так и не
        ///     сработало
        /// </exception>
        public void SeekForMatches(TextReader Reader, IList<IExpectation> Expectations)
        {
            string buffer = string.Empty;
            int readedData;
            while ((readedData = Reader.Read()) >= 0)
            {
                Console.Write((char)readedData);
                buffer += (char)readedData;
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
            throw new NoMatchesFoundException(buffer, Expectations.Select(e => e.ToString()).ToList());
        }
    }

    public static class RegexSurferHelper
    {
        /// <summary>Начинает поиск ожиданий в выводе приложения</summary>
        /// <param name="Surfer">Инструмент для поиска</param>
        /// <param name="Reader"><see cref="TextReader" /> для чтения из потока вывода приложения</param>
        /// <param name="Expectations">Список ожиданий от приложения</param>
        /// <exception cref="NoMatchesFoundException">
        ///     Вывод приложения был прочитан до конца, но завершающее ожидание так и не
        ///     сработало
        /// </exception>
        public static void SeekForMatches(this RegexSurfer Surfer, TextReader Reader, params IExpectation[] Expectations)
        {
            Surfer.SeekForMatches(Reader, Expectations);
        }
    }
}
