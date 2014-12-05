using System.IO;
using FirmwareBurner.BurningTools.Stk500.Parameters;

namespace FirmwareBurner.BurningTools.Stk500.Launching
{
    internal static class ToolLauncherHelper
    {
        /// <summary>Запускает программатор с указанными параметрами и возвращает поток вывода на консоль</summary>
        /// <param name="Launcher">Лаунчер</param>
        /// <param name="ToolBody">Место хранения файлов программатора</param>
        /// <param name="Parameters">Параметры для запуска программатора</param>
        /// <returns>Поток, который программатор выводит на консоль</returns>
        public static StreamReader Execute(this IToolLauncher Launcher, IToolBody ToolBody, params Stk500Parameter[] Parameters)
        {
            return Launcher.Execute(ToolBody, Parameters);
        }
    }
}