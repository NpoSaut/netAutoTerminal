using System;

namespace FirmwareBurner.Interaction
{
    /// <summary>Аргументы запроса на сохранение файла</summary>
    public class SaveFileRequestArguments : FileRequestArguments
    {
        public SaveFileRequestArguments(string Message, string DefaultFileType) : base(Message) { this.DefaultFileType = DefaultFileType; }

        /// <summary>Имя файла по-умолчанию</summary>
        public String DefaultFileName { get; set; }

        /// <summary>Тип файла по-умолчанию</summary>
        public String DefaultFileType { get; set; }
    }
}
