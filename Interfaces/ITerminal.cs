using System;
using System.Threading;
using JetBrains.Annotations;

namespace Saut.AutoTerminal.Interfaces
{
    /// <summary>Терминал</summary>
    public interface ITerminal : IDisposable
    {
        /// <summary>Лог вывода терминала</summary>
        string Log { get; }

        char Read(CancellationToken CancellationToken);

        [NotNull]
        string ReadLine(CancellationToken CancellationToken);

        [StringFormatMethod("Format")]
        void Write([NotNull] string Format, [NotNull] params object[] Arguments);

        [StringFormatMethod("Format")]
        void WriteLine([NotNull] string Format, [NotNull] params object[] Arguments);
    }

    public static class TerminalHelper
    {
        public static char Read(this ITerminal Terminal) { return Terminal.Read(CancellationToken.None); }
        public static string ReadLine(this ITerminal Terminal) { return Terminal.ReadLine(CancellationToken.None); }
        public static void WriteLine(this ITerminal Terminal) { Terminal.WriteLine(""); }
    }
}
