using System.Collections.Generic;

namespace Saut.AutoTerminal.Implementations.Cores
{
    public enum LineEndings
    {
        // ReSharper disable InconsistentNaming

        /// <summary>Unix-style line endings (\n)</summary>
        LF,

        /// <summary>Windows-style line endings (\r\n)</summary>
        CRLF

        // ReSharper restore InconsistentNaming
    }

    internal static class LineEndingsHelper
    {
        private static readonly Dictionary<LineEndings, string> _lineEndingStrings =
            new Dictionary<LineEndings, string>
            {
                { LineEndings.LF, "\n" },
                { LineEndings.CRLF, "\r\n" }
            };

        public static string GetString(this LineEndings Ending) { return _lineEndingStrings[Ending]; }
    }
}
