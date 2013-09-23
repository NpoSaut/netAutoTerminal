using System;
using System.Collections.Generic;
namespace FirmwarePacker.Dialogs
{
    public interface IFileSelector
    {
        string DefaultExtension { get; set; }
        IDictionary<string, string> Filters { get; set; }
        string Message { get; set; }
        string SelectOpen(string Default = null);
        string SelectSave(string Default = null);
    }
}
