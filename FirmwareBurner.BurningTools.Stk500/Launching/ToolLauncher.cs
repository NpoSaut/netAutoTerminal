using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FirmwareBurner.BurningTools.Stk500.Parameters;

namespace FirmwareBurner.BurningTools.Stk500.Launching
{
    public class ToolLauncher : IToolLauncher
    {
        /// <summary>Запускает программатор с указанными параметрами и возвращает поток вывода на консоль</summary>
        /// <param name="ToolBody">Место хранения файлов программатора</param>
        /// <param name="Parameters">Параметры для запуска программатора</param>
        /// <returns>Поток, который программатор выводит на консоль</returns>
        public StreamReader Execute(IToolBody ToolBody, IEnumerable<Stk500Parameter> Parameters)
        {
            var processStartInfo =
                new ProcessStartInfo(ToolBody.ExecutableFilePath, string.Join(" ", Parameters.Where(prm => prm != null).Select(prm => prm.Get())))
                {
                    WorkingDirectory = ToolBody.WorkingDirectoryPath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
            var p = new Process { StartInfo = processStartInfo };
            p.Start();
            return p.StandardOutput;
        }
    }
}