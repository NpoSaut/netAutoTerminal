using System.Collections.Generic;

namespace FirmwarePacker.Project
{
    /// <summary>Маппинг файла</summary>
    public interface IFileMap
    {
        ///// <summary>Включает мапинг в список файлов</summary>
        ///// <param name="FilesCollection">Список файлов</param>
        //void IncludeTo(ICollection<PackageFile> FilesCollection);

        IEnumerable<PackageFile> EnumerateFiles(string RootDirectory);
    }
}
