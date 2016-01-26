using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Saut.AutoTerminal.Exception;

namespace Saut.AutoTerminal
{
    public class RegexSeeker
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

    public static class RegexSeekerHelper
    {
        public static void SeekForMatches(this RegexSeeker Seeker, TextReader Reader, params IExpectation[] Expectations)
        {
            Seeker.SeekForMatches(Reader, Expectations);
        }
    }
}
