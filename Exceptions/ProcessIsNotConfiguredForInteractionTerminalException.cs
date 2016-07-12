using System;
using System.Runtime.Serialization;

namespace Saut.AutoTerminal.Exceptions
{
    /// <Summary>Процесс не был сконфигурирован для работы с терминалом перед запуском</Summary>
    [Serializable]
    public class ProcessIsNotConfiguredForInteractionTerminalException : TerminalException
    {
        public const string ExceptionMessage = "Процесс {0} не был сконфигурирован для работы с терминалом перед запуском";

        public ProcessIsNotConfiguredForInteractionTerminalException(string ProcessName) : base(String.Format(ExceptionMessage, ProcessName))
        {
            this.ProcessName = ProcessName;
        }

        protected ProcessIsNotConfiguredForInteractionTerminalException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }

        public string ProcessName { get; set; }
    }
}
