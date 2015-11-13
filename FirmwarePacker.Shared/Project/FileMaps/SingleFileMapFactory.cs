using System.Collections.Generic;

namespace FirmwarePacker.Project.FileMaps
{
    internal class SingleFileMapFactory : IFileMapFactory
    {
        public IFileMap CreateFileMap(IDictionary<string, string> Parameters) { return new SingleFileMap(Parameters["From"], Parameters["To"]); }
    }
}
