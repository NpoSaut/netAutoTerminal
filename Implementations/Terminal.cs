﻿using System;
using System.Diagnostics;
using System.Text;
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
            get { return _logBuilder.ToString(); }
        }

        public char Read()
        {
            try
            {
                int x = _streamTerminal.Output.Read();
                if (x < 0)
                    throw new Exception();
                var character = (char)x;
                _logBuilder.Append(character);
                Debug.Write(character);
                return character;
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
