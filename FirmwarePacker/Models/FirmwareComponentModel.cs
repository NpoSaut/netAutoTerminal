using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwarePacker.Models
{
    public class FirmwareComponentModel : ViewModel, IDataCheck
    {
        public ModuleSelectorModel TargetModule { get; set; }
        
        private FirmwareTreeModel _Tree;
        public FirmwareTreeModel Tree
        {
            get { return _Tree; }
            set
            {
                if (_Tree != value)
                {
                    _Tree = value;
                    OnPropertyChanged("Tree");
                }
            }
        }

        public FirmwareComponentModel()
        {
            TargetModule = new ModuleSelectorModel(MainModel.Index);
            Tree = new FirmwareTreeModel();
        }

        public bool Check()
        {
            return
                TargetModule.Check() &&
                Tree.Check();
        }

        public FirmwareComponentModel DeepClone()
        {
            return
                new FirmwareComponentModel()
                {
                    TargetModule = TargetModule.DeepClone(),
                    Tree = Tree.DeepClone()
                };
        }
    }
}
