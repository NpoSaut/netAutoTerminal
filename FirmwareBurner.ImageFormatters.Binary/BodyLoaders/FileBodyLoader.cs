using System.IO;

namespace FirmwareBurner.ImageFormatters.Binary.BodyLoaders
{
    public class FileBodyLoader : IBodyLoader
    {
        private readonly string _fileName;
        public FileBodyLoader(string FileName) { _fileName = FileName; }

        public byte[] LoadBootloaderBody() { return File.ReadAllBytes(_fileName); }
    }
}
