using System;
using System.IO;
using System.Text;
using Saut.AutoTerminal.Interfaces;

namespace Saut.AutoTerminal.Tests
{
    public class ConstantTerminalCore : IStreamTerminalCore
    {
        private readonly StringBuilder _sb;

        public ConstantTerminalCore(string Text)
        {
            _sb = new StringBuilder();
            Output = new StringReader(Text);
            Input = new StringWriter(_sb);
        }

        /// <summary>Лог введённых команд</summary>
        public String InputLog
        {
            get { return _sb.ToString(); }
        }

        public void Dispose() { }

        /// <summary>Вывод терминала</summary>
        public TextReader Output { get; private set; }

        /// <summary>Ввод терминала</summary>
        public TextWriter Input { get; private set; }
    }
}
