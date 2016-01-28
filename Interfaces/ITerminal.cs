using System;
using System.IO;

namespace Saut.AutoTerminal.Interfaces
{
    public interface ITerminal : IDisposable
    {
        StreamReader Output { get; }
        StreamWriter Input { get; }
    }
}
