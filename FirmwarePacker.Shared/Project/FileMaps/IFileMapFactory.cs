using System.Collections.Generic;

namespace FirmwarePacker.Project.FileMaps
{
    internal interface IFileMapFactory
    {
        IFileMap CreateFileMap(IDictionary<string, string> Parameters);
    }
}
