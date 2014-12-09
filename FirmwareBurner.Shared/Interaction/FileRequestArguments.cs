using System;
using System.Collections.Generic;

namespace FirmwareBurner.Interaction
{
    /// <summary>Аргументы запроса на операцию с файлами</summary>
    public abstract class FileRequestArguments
    {
        protected FileRequestArguments(string Message) { this.Message = Message; }

        /// <summary>Сообщение</summary>
        public String Message { get; set; }

        /// <summary>Подробное описание</summary>
        public String Description { get; set; }

        /// <summary>Поддерживаемые типы файлов</summary>
        public ICollection<FileTypeDescription> FileTypes { get; set; }

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
