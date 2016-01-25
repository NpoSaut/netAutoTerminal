using System.IO;
using System.IO.Ports;

namespace Saut.AutoTerminal
{
    public class SerialPortTerminal : ITerminal
    {
        private readonly SerialPort _port;

        public SerialPortTerminal(SerialPort Port)
        {
            _port = Port;
            Output = new StreamReader(_port.BaseStream);
            Input = new StreamWriter(_port.BaseStream);
        }

        public StreamReader Output { get; private set; }
        public StreamWriter Input { get; private set; }

        public void Dispose() { _port.Dispose(); }
    }
}
