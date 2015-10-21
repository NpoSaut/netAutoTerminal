using System.IO;

namespace FirmwareBurner.BurningTools.OpenOcd.Parameters
{
    /// <summary>Параметр, указывающий расположение папки скриптов OpenOCD</summary>
    [ParameterKind(OpenOcdParameterKind.Search)]
    internal class ScriptsLocationOpenOcdLaunchParameter : OpenOcdLaunchParameter
    {
        private readonly DirectoryInfo _scriptsDirectory;

        public ScriptsLocationOpenOcdLaunchParameter(string ScriptsDirectoryPath) : this(new DirectoryInfo(ScriptsDirectoryPath)) { }
        public ScriptsLocationOpenOcdLaunchParameter(DirectoryInfo ScriptsDirectory) { _scriptsDirectory = ScriptsDirectory; }

        /// <summary>Возвращает содержимое параметра</summary>
        protected override string GetParameterContent() { return ProcessFilePath(_scriptsDirectory.FullName); }
    }
}
