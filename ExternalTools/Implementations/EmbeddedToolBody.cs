using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ExternalTools.Interfaces;

namespace ExternalTools.Implementations
{
    /// <summary>Тело утилиты, внедрённое в ресурсы сборки</summary>
    public class EmbeddedToolBody : IToolBody
    {
        private readonly DirectoryInfo _bodyDirectory;
        private readonly string _executableFileName;

        /// <summary>Создаёт тело утилиты, внедрённое в ресурсы сборки</summary>
        /// <param name="ContainingAssembly">Сборка, содержащая тело утилиты</param>
        /// <param name="BodyNamespacePath">Путь к к ресурсу папки, содержащей утилиту</param>
        /// <param name="ExecutableFileName">Относительный путь к выполняемому файлу утилиты</param>
        public EmbeddedToolBody(Assembly ContainingAssembly, string BodyNamespacePath, string ExecutableFileName)
        {
            _executableFileName = ExecutableFileName;
            IEnumerable<string> resourceNames = ContainingAssembly
                .GetManifestResourceNames()
                .Where(resourceName => resourceName.StartsWith(BodyNamespacePath));

            _bodyDirectory = GetTemporaryDirectory();
            foreach (string resourceName in resourceNames)
            {
                string fileName = resourceName.Substring(BodyNamespacePath.Length).TrimStart('.');
                Stream resourceStream = ContainingAssembly.GetManifestResourceStream(resourceName);
                using (FileStream fileStream = File.Create(Path.Combine(WorkingDirectoryPath, fileName)))
                {
                    resourceStream.CopyTo(fileStream);
                }
            }
        }

        /// <summary>.exe-файл приложения</summary>
        public string ExecutableFilePath
        {
            get { return Path.Combine(WorkingDirectoryPath, _executableFileName); }
        }

        /// <summary>Рабочая директория</summary>
        public string WorkingDirectoryPath
        {
            get { return _bodyDirectory.FullName; }
        }

        /// <summary>
        ///     Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых
        ///     ресурсов.
        /// </summary>
        public void Dispose() { _bodyDirectory.Delete(true); }

        private DirectoryInfo GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var res = new DirectoryInfo(tempDirectory);
            res.Create();
            return res;
        }
    }
}
