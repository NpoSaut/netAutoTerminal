using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace FirmwarePacker.Models
{
    public class FirmwareTreeViewModel : ViewModel, IDataCheck
    {
        public DirectoryInfo RootDirectory { get; private set; }
        private List<FileInfo> _Files { get; set; }
        public ReadOnlyCollection<FileInfo> Files { get; private set; }

        public FirmwareTreeViewModel()
        { }
        public FirmwareTreeViewModel(DirectoryInfo di)
            : this()
        {
            ReScanFiles(di);
        }
        public FirmwareTreeViewModel(String Path)
            : this(String.IsNullOrEmpty(Path) ? null : new DirectoryInfo(Path))
        { }

        private void ReScanFiles(DirectoryInfo di)
        {
            try
            {
                _Files = di.EnumerateFiles("*", SearchOption.AllDirectories).ToList();
                RootDirectory = di;
            }
            catch
            {
                _Files = new List<FileInfo>();
                System.Windows.MessageBox.Show("Нет доступа к файлу или подпапке в выбраной директории.\nНевозможно использовать эту директорию.", "Ошибка доступа");
            }
            Files = new ReadOnlyCollection<FileInfo>(_Files);
        }

        public string Totals
        {
            get
            {
                if (RootDirectory == null) return "";
                return string.Format("{0} файл{1} ({2})", Files.Count, GetEnding(Files.Count), FirmwarePacking.FirmwareFile.GetLetteredFileSize(Files.Sum(f => f.Length)));
            }
        }

        private static string GetEnding(int count)
        {
            var c = count % 10;
            if (count >= 11 && count <= 14) return "ов";
            else if (c == 1) return "";
            else if (c >= 2 && c <= 4) return "а";
            else return "ов";
        }

        public override string ToString()
        {
            if (RootDirectory != null) return RootDirectory.FullName;
            else return "папка не выбрана";
        }

        public bool Check()
        {
            return
                RootDirectory != null &&
                Files.Any();
        }

        public FirmwareTreeViewModel DeepClone()
        {
            return RootDirectory != null ?
                new FirmwareTreeViewModel(RootDirectory.FullName) :
                new FirmwareTreeViewModel();
        }
    }
}
