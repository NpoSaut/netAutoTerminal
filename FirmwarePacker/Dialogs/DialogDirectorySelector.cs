using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwarePacker.Dialogs
{
    public class DialogDirectorySelector : FirmwarePacker.Dialogs.IDirectorySelector
    {
        public String Message { get; set; }

        public DialogDirectorySelector()
        {

        }

        public String SelectDirectory(String DefaultPlace = null)
        {
            var dlg =
                new System.Windows.Forms.FolderBrowserDialog()
                {
                    Description = Message
                };
            if (DefaultPlace != null) dlg.SelectedPath = DefaultPlace;
            return (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) ? dlg.SelectedPath : null;
        }
    }
}
