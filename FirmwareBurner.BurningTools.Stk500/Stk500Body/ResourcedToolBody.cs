using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FirmwareBurner.BurningTools.Stk500.Launching;

namespace FirmwareBurner.BurningTools.Stk500.Stk500Body
{
    public class ResourcedToolBody : IToolBody
    {
        private readonly DirectoryInfo _bodyDirectory;

        public ResourcedToolBody()
        {
            Assembly assembly = Assembly.GetAssembly(typeof (Stk500BurningTool));
            string bodyResourcesPath = GetType().Namespace;
            IEnumerable<string> resourceNames = assembly
                .GetManifestResourceNames()
                .Where(resourceName => resourceName.StartsWith(bodyResourcesPath));

            _bodyDirectory = GetTemporaryDirectory();
            foreach (string resourceName in resourceNames)
            {
                string fileName = resourceName.Substring(bodyResourcesPath.Length).TrimStart('.');
                Stream resourceStream = assembly.GetManifestResourceStream(resourceName);
                using (var fileStream = File.Create(Path.Combine(WorkingDirectoryPath, fileName)))
                {
                    resourceStream.CopyTo(fileStream);
                }
            }
        }

        /// <summary>.exe-файл приложения</summary>
        public string ExecutableFilePath
        {
            get { return Path.Combine(WorkingDirectoryPath, "Stk500.exe"); }
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

        public DirectoryInfo GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var res = new DirectoryInfo(tempDirectory);
            res.Create();
            return res;
        }
    }
}
