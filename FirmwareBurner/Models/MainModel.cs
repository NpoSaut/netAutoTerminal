using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwarePacker.Models;

namespace FirmwareBurner.Models
{
    public class MainModel : ViewModel
    {
        public static FirmwarePacking.SystemsIndexes.Index Index { get; private set; }

        public ModuleSelectorModel Module { get; set; }

        public MainModel()
        {
            Module = new ModuleSelectorModel(Index);
        }

        static MainModel()
        {
            Index = new FirmwarePacking.SystemsIndexes.XmlIndex("BlockKinds.xml");
        }
    }
}
