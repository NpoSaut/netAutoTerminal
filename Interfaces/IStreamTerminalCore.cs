using System;
using System.IO;

namespace Saut.AutoTerminal.Interfaces
{
    public interface IStreamTerminalCore : IDisposable
    {
        /// <summary>Вывод терминала</summary>
        TextReader Output { get; }

        /// <summary>Ввод терминала</summary>
        TextWriter Input { get; }
    }
}
