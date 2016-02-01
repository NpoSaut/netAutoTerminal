using System;
using System.IO;

namespace Saut.AutoTerminal.Interfaces
{
    /// <summary>Терминал</summary>
    public interface ITerminal : IDisposable
    {
        /// <summary>Вывод терминала</summary>
        StreamReader Output { get; }

        /// <summary>Ввод терминала</summary>
        StreamWriter Input { get; }
    }
}
