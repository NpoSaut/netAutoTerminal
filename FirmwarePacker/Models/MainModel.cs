using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace FirmwarePacker.Models
{
    public class MainModel : ViewModel
    {
        public static FirmwarePacking.SystemsIndexes.Index Index { get; set; }

        public ObservableCollection<FirmwareComponentModel> Components { get; set; }

        public MainModel()
        {
            Components =
                new ObservableCollection<FirmwareComponentModel>()
                {
                    new FirmwareComponentModel()
                };
        }

        static MainModel()
        {
            Index = new FirmwarePacking.SystemsIndexes.XmlIndex("BlockKinds.xml");
        }
    }
}
