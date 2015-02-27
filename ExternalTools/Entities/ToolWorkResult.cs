namespace ExternalTools.Entities
{
    /// <summary>Результат выполнения программы</summary>
    public class ToolWorkResult
    {
        public ToolWorkResult(string Output) : this(Output, string.Empty) { }

        public ToolWorkResult(string Output, string Errors)
        {
            this.Output = Output;
            this.Errors = Errors;
        }

        /// <summary>Содержимое стандартного выходного потока</summary>
        public string Output { get; private set; }

        /// <summary>Содержимое стандартного потока ошибок</summary>
        public string Errors { get; private set; }
    }
}
