using System.Collections.Generic;
using System.IO;

namespace FirmwarePacker.Project.FileMaps
{
    internal class SingleFileMap : FileMap
    {
        private readonly string _destinationName;
        private readonly string _sourceFileName;

        public SingleFileMap(string SourceFileName, string DestinationName)
        {
            _sourceFileName = SourceFileName;
            _destinationName = DestinationName;
        }

        protected override IEnumerable<MapingItem> EnumerateMapingItems(string RootDirectory)
        {
            yield return new MapingItem(new FileInfo(Path.Combine(RootDirectory, _sourceFileName)), _destinationName);
        }

        public override string ToString() { return string.Format("{0} -> {1}", _sourceFileName, _destinationName); }
    }
}
