using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FirmwarePacker.Project.FileMaps
{
    internal abstract class FileMap : IFileMap
    {
        public IEnumerable<PackageFile> EnumerateFiles()
        {
            return EnumerateMapingItems().Select(item => new PackageFile(item.PackageFileName, File.ReadAllBytes(item.LocalFile.FullName)));
        }

        protected abstract IEnumerable<MapingItem> EnumerateMapingItems();

        protected class MapingItem
        {
            public MapingItem(FileInfo LocalFile, string PackageFileName)
            {
                this.LocalFile = LocalFile;
                this.PackageFileName = PackageFileName;
            }

            public FileInfo LocalFile { get; private set; }
            public string PackageFileName { get; private set; }
        }
    }
}
