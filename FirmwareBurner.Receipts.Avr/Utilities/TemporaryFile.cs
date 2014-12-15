using System;
using System.IO;

namespace FirmwareBurner.Receipts.Avr.Utilities
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
            var fi = new FileInfo(Path.GetTempFileName());
            if (ContentStream != null)
            {
                using (FileStream fs = fi.OpenWrite())
                {
                    ContentStream.CopyTo(fs);
                }
            }
        }

        public FileInfo FileInfo { get; private set; }

        public void Dispose() { FileInfo.Delete(); }
    }
}
