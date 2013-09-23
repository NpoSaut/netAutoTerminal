using System;
namespace FirmwarePacker.Dialogs
{
    public interface IDirectorySelector
    {
        string Message { get; set; }
        string SelectDirectory(string DefaultPlace = null);
    }
}
