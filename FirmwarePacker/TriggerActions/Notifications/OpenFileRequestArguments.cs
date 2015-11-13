using System;

namespace FirmwarePacker.TriggerActions.Notifications
{
    /// <summary>Аргументы запроса на открытие файла</summary>
    public class OpenFileRequestArguments : FileRequestArguments
    {
        public OpenFileRequestArguments(string Message, string DefaultFileType) : base(Message) { this.DefaultFileType = DefaultFileType; }

        /// <summary>Имя файла по-умолчанию</summary>
        public String DefaultFileName { get; set; }

        /// <summary>Тип файла по-умолчанию</summary>
        public String DefaultFileType { get; set; }
    }
}