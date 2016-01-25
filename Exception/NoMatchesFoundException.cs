using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Saut.AutoTerminal.Exception
{
    /// <Summary>Не найдено совпадений и искомыми шаблонами</Summary>
    [Serializable]
    public class NoMatchesFoundException : ApplicationException
    {
        public NoMatchesFoundException(string Text, List<string> Expectations) : base("Не найдено совпадений и искомыми шаблонами")
        {
            this.Text = Text;
            this.Expectations = Expectations;
        }

        protected NoMatchesFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }

        public string Text { get; set; }
        public List<string> Expectations { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder(base.ToString());

            sb.AppendLine("Искомые шаблоны:");
            foreach (string expectation in Expectations)
                sb.AppendLine(expectation);

            sb.AppendLine("Текст:");
            sb.AppendLine(Text);
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
