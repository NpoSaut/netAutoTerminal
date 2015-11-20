using System.IO;
using ExternalTools.Interfaces;

namespace ExternalTools.Implementations
{
    public class StaticToolBodyFactory : IToolBodyFactory
    {
        private readonly FileInfo _executableFile;
        private readonly DirectoryInfo _toolRoot;

        public StaticToolBodyFactory(string ToolRootPath, string ExecutableFileRelativePath)
            : this(new DirectoryInfo(ToolRootPath), ExecutableFileRelativePath) { }

        public StaticToolBodyFactory(DirectoryInfo ToolRoot, string ExecutableFileRelativePath)
            : this(ToolRoot, new FileInfo(Path.Combine(ToolRoot.FullName, ExecutableFileRelativePath))) { }

        public StaticToolBodyFactory(string ExecutableFilePath) : this(new FileInfo(ExecutableFilePath)) { }
        public StaticToolBodyFactory(FileInfo ExecutableFile) : this(ExecutableFile.Directory, ExecutableFile) { }

        public StaticToolBodyFactory(DirectoryInfo ToolRoot, FileInfo ExecutableFile)
        {
            _toolRoot = ToolRoot;
            _executableFile = ExecutableFile;
        }

        public IToolBody GetToolBody() { return new StaticToolBody(_toolRoot, _executableFile); }
    }
}
