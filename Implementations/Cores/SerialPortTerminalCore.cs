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

        public SerialPortTerminalCore([NotNull] SerialPort Port, Encoding Encoding, LineEndings Ending = LineEndings.LF, int BaudRate = 115200)
        {
            _port = Port;
            _port.Open();
            _port.ReadTimeout = 10000;
            _port.BaudRate = BaudRate;
            Output = new StreamReader(_port.BaseStream, Encoding);
            Input = new StreamWriter(_port.BaseStream, Encoding) { AutoFlush = true, NewLine = Ending.GetString() };
        }

        public TextReader Output { get; private set; }
        public TextWriter Input { get; private set; }

        public void Dispose() { _port.Dispose(); }
    }
}
