using System.IO;
using ExternalTools.Interfaces;

namespace ExternalTools.Implementations
{
    /// <summary>Тело утилиты, спокойно лежащее себе на диске</summary>
    public class StaticToolBody : IToolBody
    {
        private readonly FileInfo _executableFile;
        private readonly DirectoryInfo _toolRoot;

        public StaticToolBody(DirectoryInfo ToolRoot, FileInfo ExecutableFile)
        {
            _toolRoot = ToolRoot;
            _executableFile = ExecutableFile;
        }

        public void Dispose() { }

        /// <summary>.exe-файл приложения</summary>
        public string ExecutableFilePath
        {
            get { return _executableFile.FullName; }
        }

        /// <summary>Рабочая директория</summary>
        public string WorkingDirectoryPath
        {
            get { return _toolRoot.FullName; }
        }
    }
}
