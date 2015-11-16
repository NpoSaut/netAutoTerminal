using System;

namespace FirmwarePacker.TriggerActions.Notifications
{
    /// <summary>Аргументы запроса на сохранение файла</summary>
    public class SaveFileRequestArguments : FileRequestArguments
    {
        public SaveFileRequestArguments(string DefaultFileType, params FileTypeDescription[] FileTypes) : base(FileTypes)
        {
            this.DefaultFileType = DefaultFileType;
        }

        /// <summary>Имя файла по-умолчанию</summary>
        public String DefaultFileName { get; set; }

        /// <summary>Тип файла по-умолчанию</summary>
        public String DefaultFileType { get; set; }
    }
}
