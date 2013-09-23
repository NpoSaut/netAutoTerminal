using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FirmwarePacker.Models
{
    public class FirmwareComponentModel : ViewModel, IDataCheck
    {
        [Microsoft.Practices.Unity.Dependency]
        public ModuleSelectorModel TargetModule { get; set; }

        public ICommand SelectTreeCommand { get; set; }
        public ICommand CloneCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        private FirmwareTreeModel _Tree;
        [Microsoft.Practices.Unity.Dependency]
        public FirmwareTreeModel Tree
        {
            get { return _Tree; }
            set
            {
                if (_Tree != value)
                {
                    _Tree = value;
                    OnTreeChanged();
                    OnPropertyChanged("Tree");
                }
            }
        }
        private void OnTreeChanged()
        {
            if (TreeChanged != null) TreeChanged(this, new EventArgs());
        }

        public FirmwareComponentModel()
        {
            SelectTreeCommand = new ActionCommand(SelectTree);
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
                    selector = selector,
                    TargetModule = TargetModule.DeepClone(),
                    Tree = Tree.DeepClone()
                };
        }

        [Microsoft.Practices.Unity.Dependency]
        public Dialogs.IDirectorySelector selector { get; set; }
        private void SelectTree()
        {
            var DirectoryPath = selector.SelectDirectory();
            if (DirectoryPath != null)
            {
                Tree = new FirmwareTreeModel(DirectoryPath);
            }
        }

        public event EventHandler TreeChanged;
    }
}
