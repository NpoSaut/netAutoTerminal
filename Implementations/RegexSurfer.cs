using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Saut.AutoTerminal.Exception;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations
{
    public class RegexSurfer
    {
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
        public static void SeekForMatches(this RegexSurfer Surfer, TextReader Reader, params IExpectation[] Expectations)
        {
            Surfer.SeekForMatches(Reader, Expectations);
        }
    }
}
