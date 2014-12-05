using System;

namespace FirmwareBurner.BurningTools.Stk500.Launching
{
    /// <summary>Место хранения файлов программатора</summary>
    public interface IToolBody : IDisposable
    {
        /// <summary>.exe-файл приложения</summary>
        String ExecutableFilePath { get; }

        /// <summary>Рабочая директория</summary>
        String WorkingDirectoryPath { get; }
    }
}
