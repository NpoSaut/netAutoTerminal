using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FirmwarePacker.Project.FileMaps
{
    internal class FolderMap : FileMap
    {
        private readonly string _destinationFolder;
        private readonly string _searchPattern;
        private readonly string _sourceFolder;

        public FolderMap(string SourceFolder, string SearchPattern, string DestinationFolder)
        {
            _sourceFolder = SourceFolder.TrimEnd(Path.DirectorySeparatorChar);
            _destinationFolder = DestinationFolder.TrimEnd('/');
            _searchPattern = SearchPattern ?? "*";
        }

        protected override IEnumerable<MapingItem> EnumerateMapingItems()
        {
            var source = new DirectoryInfo(_sourceFolder);
            int fullNameOffset = source.FullName.Length;
            return source.EnumerateFiles(_searchPattern, SearchOption.AllDirectories)
                         .Select(f => new MapingItem(f,
                                                     String.Format("{0}/{1}",
                                                                   _destinationFolder,
                                                                   f.FullName.Substring(fullNameOffset).Replace(Path.DirectorySeparatorChar, '/'))));
        }
    }
}
