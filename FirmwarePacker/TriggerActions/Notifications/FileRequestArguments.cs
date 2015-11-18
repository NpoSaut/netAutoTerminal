using System;
using System.Collections.Generic;

namespace FirmwarePacker.TriggerActions.Notifications
{
    /// <summary>Аргументы запроса на операцию с файлами</summary>
    public abstract class FileRequestArguments
    {
        protected FileRequestArguments(params FileTypeDescription[] FileTypes) { this.FileTypes = FileTypes; }

        /// <summary>Поддерживаемые типы файлов</summary>
        public ICollection<FileTypeDescription> FileTypes { get; private set; }

        /// <summary>Описание типа файла</summary>
        public class FileTypeDescription
        {
            /// <summary>Создаёт экземпляр описания типа файлов</summary>
            /// <param name="Extension">Расширение файла</param>
            /// <param name="Description">Описание типа файла</param>
            public FileTypeDescription(string Extension, string Description)
            {
                this.Extension = Extension;
                this.Description = Description;
            }

            /// <summary>Расширение файла</summary>
            public String Extension { get; private set; }

            /// <summary>Описание типа файла</summary>
            public String Description { get; private set; }
        }
    }
}
