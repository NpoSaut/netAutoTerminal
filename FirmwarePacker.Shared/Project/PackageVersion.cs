using System;

namespace FirmwarePacker.Project
{
    /// <summary>Версия пакета ПО</summary>
    public class PackageVersion
    {
        public PackageVersion(int Major, int Minor, string Label, DateTime ReleaseDate)
        {
            this.ReleaseDate = ReleaseDate;
            this.Major = Major;
            this.Minor = Minor;
            this.Label = Label;
        }

        /// <summary>Версия</summary>
        public int Major { get; private set; }

        /// <summary>Подверсия</summary>
        public int Minor { get; private set; }

        /// <summary>Текстовая метка</summary>
        public string Label { get; private set; }

        /// <summary>Дата выпуска пакета</summary>
        public DateTime ReleaseDate { get; private set; }
    }
}
