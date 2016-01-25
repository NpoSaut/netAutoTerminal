using System;
using System.IO;

namespace Saut.AutoTerminal
{
    public interface ITerminal : IDisposable
    {
        StreamReader Output { get; }
        StreamWriter Input { get; }
    }
}
