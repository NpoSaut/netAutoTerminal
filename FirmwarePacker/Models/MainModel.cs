using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwarePacker.Models
{
    public class MainModel : ViewModel
    {
        public FirmwarePacking.SystemsIndexes.Index Index { get; set; }
        public ModuleSelectorModel ModuleSelector { get; set; }

        public MainModel()
        {
            Index = new FirmwarePacking.SystemsIndexes.XmlIndex("BlockKinds.xml");
            ModuleSelector = new ModuleSelectorModel(Index);
        }
    }
}
