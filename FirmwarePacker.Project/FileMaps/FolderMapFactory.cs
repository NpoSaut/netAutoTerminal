using System.Collections.Generic;

namespace FirmwarePacker.Project.FileMaps
{
    internal class FolderMapFactory : IFileMapFactory
    {
        public IFileMap CreateFileMap(IDictionary<string, string> Parameters)
        {
            return new FolderMap(Parameters["From"], Parameters["Pattern"], Parameters["To"]);
        }
    }
}
