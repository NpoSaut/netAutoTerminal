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

        protected override IEnumerable<MapingItem> EnumerateMapingItems() { yield return new MapingItem(new FileInfo(_sourceFileName), _destinationName); }
    }
}
