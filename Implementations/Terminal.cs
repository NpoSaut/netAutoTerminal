using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Saut.AutoTerminal.Exceptions;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Implementations
{
    public class Terminal : ITerminal
    {
        private readonly StringBuilder _logBuilder = new StringBuilder();
        private readonly IStreamTerminalCore _streamTerminal;
        public Terminal(IStreamTerminalCore StreamTerminal) { _streamTerminal = StreamTerminal; }

        public string Log
        {
            get { return _logBuilder.ToString().Trim('\0'); }
        }

        public char Read(CancellationToken CancellationToken)
        {
            try
            {
                int x = _streamTerminal.Output.Read();
                if (x < 0)
                    throw new TerminalStreamEndedTerminalException(Log);
                var character = (char)x;
                _logBuilder.Append(character);
                //Debug.Write(character);
                return character;
            }
            catch (TimeoutException)
            {
                throw new TimeoutTerminalException();
            }
        }

        public string ReadLine(CancellationToken CancellationToken)
        {
            try
            {
                string line = _streamTerminal.Output.ReadLine();
                if (line == null)
                    throw new Exception();
                _logBuilder.AppendLine(line);
                Debug.WriteLine(line);
                return line;
            }
            catch (TimeoutException)
            {
                throw new TimeoutTerminalException();
            }
        }

        public void Write(string Format, params object[] Arguments) { _streamTerminal.Input.Write(Format, Arguments); }
        public void WriteLine(string Format, params object[] Arguments) { _streamTerminal.Input.WriteLine(Format, Arguments); }

        public void Dispose() { _streamTerminal.Dispose(); }
    }
}
