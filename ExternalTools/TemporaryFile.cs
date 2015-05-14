using System;
using System.IO;

namespace ExternalTools
{
    /// <summary>Временный файл</summary>
    /// <remarks>
    ///     Класс позволяет создать временный файл с содержимым, переданным через конструктор и удалить его при вызове
    ///     метода <see cref="IDisposable.Dispose" />
    /// </remarks>
    public class TemporaryFile : IDisposable
    {
        public TemporaryFile(Stream ContentStream)
        {
            FileInfo = new FileInfo(Path.GetTempFileName());
            if (ContentStream != null)
            {
                using (FileStream fs = FileInfo.OpenWrite())
                {
                    ContentStream.CopyTo(fs);
                }
            }
        }

        public FileInfo FileInfo { get; private set; }

        public void Dispose() { FileInfo.Delete(); }
    }
}
