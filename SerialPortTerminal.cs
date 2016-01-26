using System.IO;
using System.IO.Ports;
using System.Text;

namespace Saut.AutoTerminal
{
    public class SerialPortTerminal : ITerminal
    {
        private readonly SerialPort _port;

        public SerialPortTerminal(SerialPort Port)
        {
            _port = Port;
            _port.Open();
            Output = new StreamReader(_port.BaseStream, Encoding.UTF8);
            Input = new StreamWriter(_port.BaseStream, Encoding.UTF8) { AutoFlush = true };
        }

        public StreamReader Output { get; private set; }
        public StreamWriter Input { get; private set; }

        public void Dispose() { _port.Dispose(); }
    }
}
