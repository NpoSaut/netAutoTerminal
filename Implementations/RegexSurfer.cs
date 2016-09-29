using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        /// <param name="CancellationToken">Токен отмены поиска</param>
        /// <param name="Timeout">Таймаут операции поиска</param>
        /// <param name="Expectations">Список ожиданий от приложения</param>
        /// <exception cref="NoMatchesFoundException">
        ///     Вывод приложения был прочитан до конца, но завершающее ожидание так и не
        ///     сработало
        /// </exception>
        public void SeekForMatches(ITerminal Terminal, CancellationToken CancellationToken, TimeSpan Timeout, IList<IExpectation> Expectations)
        {
            //var surfingMethod = Expectations.Min(e => e.RequiredSurfingMethod);
            const SurfingMethod surfingMethod = SurfingMethod.ByCharacter;
            var buffer = new StringBuilder();
            try
            {
                while (true)
                {
                    CancellationToken.ThrowIfCancellationRequested();
                    switch (surfingMethod)
                    {
                        case SurfingMethod.ByCharacter:
                            buffer.Append(Terminal.Read(CancellationToken, Timeout));
                            break;
                        case SurfingMethod.ByLine:
                            buffer.AppendLine(Terminal.ReadLine(CancellationToken, Timeout));
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
        public static TimeSpan DefaultTimeout
        {
            get { return TerminalHelper.DefaultTimeout; }
            set { TerminalHelper.DefaultTimeout = value; }
        }

        /// <summary>Начинает поиск ожиданий в выводе приложения</summary>
        /// <param name="Surfer">Инструмент для поиска</param>
        /// <param name="Terminal">Терминал для чтения</param>
        /// <param name="Expectations">Список ожиданий от приложения</param>
        /// <exception cref="NoMatchesFoundException">
        ///     Вывод приложения был прочитан до конца, но завершающее ожидание так и не
        ///     сработало
        /// </exception>
        public static void SeekForMatches(this RegexSurfer Surfer, ITerminal Terminal,
                                          params IExpectation[] Expectations)
        {
            SeekForMatches(Surfer, Terminal, DefaultTimeout, Expectations);
        }

        /// <summary>Начинает поиск ожиданий в выводе приложения</summary>
        /// <param name="Surfer">Инструмент для поиска</param>
        /// <param name="Terminal">Терминал для чтения</param>
        /// <param name="Timeout">Таймаут операции поиска</param>
        /// <param name="Expectations">Список ожиданий от приложения</param>
        /// <exception cref="NoMatchesFoundException">
        ///     Вывод приложения был прочитан до конца, но завершающее ожидание так и не
        ///     сработало
        /// </exception>
        public static void SeekForMatches(this RegexSurfer Surfer, ITerminal Terminal, TimeSpan Timeout,
                                          params IExpectation[] Expectations)
        {
            SeekForMatches(Surfer, Terminal, CancellationToken.None, Timeout, Expectations);
        }

        /// <summary>Начинает поиск ожиданий в выводе приложения</summary>
        /// <param name="Surfer">Инструмент для поиска</param>
        /// <param name="Terminal">Терминал для чтения</param>
        /// <param name="CancellationToken">Токен для отмены ожидания</param>
        /// <param name="Expectations">Список ожиданий от приложения</param>
        /// <exception cref="NoMatchesFoundException">
        ///     Вывод приложения был прочитан до конца, но завершающее ожидание так и не
        ///     сработало
        /// </exception>
        public static void SeekForMatches(this RegexSurfer Surfer, ITerminal Terminal, CancellationToken CancellationToken,
                                          params IExpectation[] Expectations)
        {
            SeekForMatches(Surfer, Terminal, CancellationToken, DefaultTimeout, Expectations);
        }

        /// <summary>Начинает поиск ожиданий в выводе приложения</summary>
        /// <param name="Surfer">Инструмент для поиска</param>
        /// <param name="Terminal">Терминал для чтения</param>
        /// <param name="CancellationToken">Токен для отмены ожидания</param>
        /// <param name="Timeout">Таймаут операции поиска</param>
        /// <param name="Expectations">Список ожиданий от приложения</param>
        /// <exception cref="NoMatchesFoundException">
        ///     Вывод приложения был прочитан до конца, но завершающее ожидание так и не
        ///     сработало
        /// </exception>
        public static void SeekForMatches(this RegexSurfer Surfer, ITerminal Terminal, CancellationToken CancellationToken, TimeSpan Timeout,
                                          params IExpectation[] Expectations)
        {
            Surfer.SeekForMatches(Terminal, CancellationToken, Timeout, Expectations);
        }
    }
}
