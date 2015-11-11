using System;

namespace FirmwarePacker.LaunchParameters
{
    /// <summary>Параметры, переданные приложению при его запуске</summary>
    public interface ILaunchParameters
    {
        /// <summary>Путь к файлу проекта</summary>
        string ProjectFileName { get; }

        /// <summary>Версия ПО в проекте</summary>
        int? VersionMajor { get; }

        /// <summary>Подверсия ПО в проекте</summary>
        int? VersionMinor { get; }

        /// <summary>Текстовая метка версии ПО в проекте</summary>
        string VersionLabel { get; }

        /// <summary>Дата сборки ПО в проекте</summary>
        DateTime? ReleaseDate { get; }

        string OutputFileName { get; }
    }
}
