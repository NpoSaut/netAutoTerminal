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

        char Read(CancellationToken CancellationToken, TimeSpan Timeout);

        [NotNull]
        string ReadLine(CancellationToken CancellationToken, TimeSpan Timeout);

        [StringFormatMethod("Format")]
        void Write([NotNull] string Format, [NotNull] params object[] Arguments);

        [StringFormatMethod("Format")]
        void WriteLine([NotNull] string Format, [NotNull] params object[] Arguments);
    }

    public static class TerminalHelper
    {
        static TerminalHelper() { DefaultTimeout = TimeSpan.FromSeconds(10); }

        public static TimeSpan DefaultTimeout { get; set; }

        public static char Read(this ITerminal Terminal) { return Read(Terminal, DefaultTimeout); }
        public static char Read(this ITerminal Terminal, TimeSpan Timeout) { return Terminal.Read(CancellationToken.None, Timeout); }
        public static string ReadLine(this ITerminal Terminal) { return ReadLine(Terminal, DefaultTimeout); }
        public static string ReadLine(this ITerminal Terminal, TimeSpan Timeout) { return Terminal.ReadLine(CancellationToken.None, Timeout); }

        public static void WriteLine(this ITerminal Terminal) { Terminal.WriteLine(""); }
    }
}
