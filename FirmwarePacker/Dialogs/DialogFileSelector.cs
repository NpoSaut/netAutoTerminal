using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace FirmwarePacker.Dialogs
{
    public class DialogFileSelector : FirmwarePacker.Dialogs.IFileSelector
    {
        public String Message { get; set; }
        public IDictionary<String, String> Filters { get; set; }
        public String DefaultExtension { get; set; }

        public DialogFileSelector()
        {
            Filters =
                new Dictionary<String, String>()
                {
                    { "Все файлы (*.*)", "*.*" }
                };
        }

        public String SelectOpen(string Default)
        {
            var Dialog = new OpenFileDialog()
            {
                Title = Message,
                FileName = Default,
                Filter = string.Join("|", Filters.Select(f => string.Format("{0} ({1})|{1}", f.Key, f.Value)))
            };
            return Dialog.ShowDialog() == true ? Dialog.FileName : null;
        }
        public String SelectSave(string Default)
        {
            var Dialog = new SaveFileDialog()
            {
                Title = Message,
                FileName = Default,
                Filter = string.Join("|", Filters.Select(f => string.Format("{0}|{1}", f.Key, f.Value)))
            };
            return Dialog.ShowDialog() == true ? Dialog.FileName : null;
        }
    }
}
