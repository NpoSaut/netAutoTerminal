using System.IO;
using System.IO.Ports;
using System.Text;
using JetBrains.Annotations;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations.Cores
{
    /// <summary>Терминал, работающий через последовательный порт</summary>
    public class SerialPortTerminalCore : IStreamTerminalCore
    {
        private readonly SerialPort _port;

        public SerialPortTerminalCore([NotNull] SerialPort Port)
        {
            _port = Port;
            _port.Open();
            _port.ReadTimeout = 10000;
            Output = new StreamReader(_port.BaseStream, Encoding.UTF8);
            Input = new StreamWriter(_port.BaseStream, Encoding.UTF8) { AutoFlush = true, NewLine = "\n" };
        }

        public TextReader Output { get; private set; }
        public TextWriter Input { get; private set; }

        public void Dispose() { _port.Dispose(); }
    }
}
