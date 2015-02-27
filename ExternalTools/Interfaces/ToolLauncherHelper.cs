using System.Diagnostics;

namespace ExternalTools.Interfaces
{
    public static class ToolLauncherHelper
    {
        /// <summary>Запускает программатор с указанными параметрами и возвращает поток вывода на консоль</summary>
        /// <param name="Launcher">Лаунчер</param>
        /// <param name="ToolBody">Место хранения файлов программатора</param>
        /// <param name="Parameters">Параметры для запуска программатора</param>
        /// <returns>Информация о процессе выполнения утилиты</returns>
        public static Process Execute(this IToolLauncher Launcher, IToolBody ToolBody, params ILaunchParameter[] Parameters)
        {
            return Launcher.Execute(ToolBody, Parameters);
        }
    }
}
