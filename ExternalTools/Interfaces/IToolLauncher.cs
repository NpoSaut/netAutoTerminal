using System.Collections.Generic;
using System.IO;

namespace ExternalTools.Interfaces
{
    /// <summary>Лаунчер программатора</summary>
    public interface IToolLauncher
    {
        /// <summary>Запускает программатор с указанными параметрами и возвращает поток вывода на консоль</summary>
        /// <param name="ToolBody">Место хранения файлов программатора</param>
        /// <param name="Parameters">Параметры для запуска программатора</param>
        /// <returns>Поток, который программатор выводит на консоль</returns>
        StreamReader Execute(IToolBody ToolBody, IEnumerable<ILaunchParameter> Parameters);
    }
}
