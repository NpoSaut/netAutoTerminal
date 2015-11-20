using System.Collections.Generic;

namespace FirmwarePacker.Project.FileMaps
{
    internal class FolderMapFactory : IFileMapFactory
    {
        public IFileMap CreateFileMap(IDictionary<string, string> Parameters)
        {
            string value;
            string searchPattern = Parameters.TryGetValue("Pattern", out value) ? value : "*";
            return new FolderMap(Parameters["From"], searchPattern, Parameters["To"]);
        }
    }
}
