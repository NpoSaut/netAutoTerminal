using System.IO;
using System.IO.Ports;
using System.Text;
using JetBrains.Annotations;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations
{
    /// <summary>Терминал, работающий через последовательный порт</summary>
    public class SerialPortTerminal : ITerminal
    {
        private readonly SerialPort _port;

        public SerialPortTerminal([NotNull] SerialPort Port)
        {
            _port = Port;
            _port.Open();
            Output = new StreamReader(_port.BaseStream, Encoding.UTF8);
            Input = new StreamWriter(_port.BaseStream, Encoding.UTF8) { AutoFlush = true, NewLine = "\n" };
        }

        public StreamReader Output { get; private set; }
        public StreamWriter Input { get; private set; }

        public void Dispose() { _port.Dispose(); }
    }
}
