using System;
using JetBrains.Annotations;

namespace Saut.AutoTerminal.Interfaces
{
    /// <summary>Терминал</summary>
    public interface ITerminal : IDisposable
    {
        /// <summary>Лог вывода терминала</summary>
        string Log { get; }

        char Read();

        [NotNull]
        string ReadLine();

        [StringFormatMethod("Format")]
        void Write([NotNull] string Format, [NotNull] params object[] Arguments);

        [StringFormatMethod("Format")]
        void WriteLine([NotNull] string Format, [NotNull] params object[] Arguments);
    }

    public static class TerminalHelper
    {
        public static void WriteLine(this ITerminal Terminal) { Terminal.WriteLine(""); }
    }
}
