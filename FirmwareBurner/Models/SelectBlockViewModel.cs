using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwarePacking.SystemsIndexes;
using FirmwarePacking;
using FirmwareBurner.Models;

namespace FirmwarePacker.Models
{
    public class ModuleSelectorModel : ViewModelBase, IDataCheck
    {
        private List<BlockKind> _BlockKinds;
        public List<BlockKind> BlockKinds
        {
            get { return _BlockKinds; }
            set
            {
                if (_BlockKinds != value)
                {
                    var oldValue = _BlockKinds;
                    _BlockKinds = value;
                    OnBlockKindsListChanged(oldValue, value);
                }
            }
        }
        private void OnBlockKindsListChanged(List<BlockKind> oldValue, List<BlockKind> value)
        {
            OnPropertyChanged("BlockKinds");
            SelectedBlockKind = GetDefault(value);
        }

        private BlockKind _SelectedBlockKind;
        public BlockKind SelectedBlockKind
        {
            get { return _SelectedBlockKind; }
            set
            {
                if (_SelectedBlockKind != value)
                {
                    var oldValue = _SelectedBlockKind;
                    _SelectedBlockKind = value;
                    OnBlockKindChanged(oldValue, value);
                }
            }
        }
        private void OnBlockKindChanged(BlockKind oldValue, BlockKind value)
        {
            OnPropertyChanged("SelectedBlockKind");
            ModuleKinds = value.Modules;
            ModificationKinds = value.Modifications;
            Channels = Enumerable.Range(1, value.ChannelsCount).Select(i => new ChannelModel(this, i)).ToList();
        }


        private List<ModuleKind> _ModuleKinds;
        public List<ModuleKind> ModuleKinds
        {
            get { return _ModuleKinds; }
            set
            {
                if (_ModuleKinds != value)
                {
                    var oldValue = _ModuleKinds;
                    _ModuleKinds = value;
                    OnModuleKindsListChanged(oldValue, value);
                }
            }
        }
        private void OnModuleKindsListChanged(List<ModuleKind> oldValue, List<ModuleKind> value)
        {
            OnPropertyChanged("ModuleKinds");
            SelectedModuleKind = GetDefault(value);
        }

        private ModuleKind _SelectedModuleKind;
        public ModuleKind SelectedModuleKind
        {
            get { return _SelectedModuleKind; }
            set
            {
                if (_SelectedModuleKind != value)
                {
                    var oldValue = _SelectedModuleKind;
                    _SelectedModuleKind = value;
                    OnModuleKindChanged(oldValue, value);
                }
            }
        }
        private void OnModuleKindChanged(ModuleKind oldValue, ModuleKind value)
        {
            OnPropertyChanged("SelectedModuleKind");
            OnSelectionChanged();
        }


        private List<ModificationKind> _ModificationKinds;
        public List<ModificationKind> ModificationKinds
        {
            get { return _ModificationKinds; }
            set
            {
                if (_ModificationKinds != value)
                {
                    var oldValue = _ModificationKinds;
                    _ModificationKinds = value;
                    OnModificationKindsListChanged(oldValue, value);
                }
            }
        }
        private void OnModificationKindsListChanged(List<ModificationKind> oldValue, List<ModificationKind> value)
        {
            OnPropertyChanged("ModificationKinds");
            SelectedModificationKind = GetDefault(value);
        }

        private ModificationKind _SelectedModificationKind;
        public ModificationKind SelectedModificationKind
        {
            get { return _SelectedModificationKind; }
            set
            {
                if (_SelectedModificationKind != value)
                {
                    _SelectedModificationKind = value;
                    OnModificationChanged();
                }
            }
        }
        private void OnModificationChanged()
        {
            OnPropertyChanged("SelectedModificationKind");
        }

        public ModuleSelectorModel(Index index)
        {
            this.BlockKinds = index.Blocks.ToList();
        }

        private List<ChannelModel> _Channels;
        public List<ChannelModel> Channels
        {
            get { return _Channels; }
            set
            {
                if (_Channels != value)
                {
                    var oldValue = _Channels;
                    _Channels = value;
                    OnChannelsListChanged(oldValue, value);
                }
            }
        }
        private void OnChannelsListChanged(List<ChannelModel> oldValue, List<ChannelModel> value)
        {
            OnPropertyChanged("Channels");
        }

        private T GetDefault<T>(List<T> inList)
        {
            //return null;
            return inList.FirstOrDefault();
        }

        public class ChannelModel : ViewModelBase
        {
            public int Id { get; private set; }
            private ModuleSelectorModel module { get; set; }

            private bool _IsSelected;
            public bool IsSelected
            {
                get { return _IsSelected; }
                set
                {
                    if (_IsSelected != value)
                    {
                        _IsSelected = value;
                        OnPropertyChanged("IsSelected");
                        module.OnSelectionChanged();
                    }
                }
            }

            public ChannelModel(ModuleSelectorModel Module, int Id, bool IsSelected)
            {
                this.module = Module;
                this.Id = Id;
                this.IsSelected = IsSelected;
            }
            public ChannelModel(ModuleSelectorModel Module, int Id)
                : this(Module, Id, true)
            { }

            public override string ToString()
            {
                return string.Format("Канал {0}", Id);
            }
        }

        public bool Check()
        {
            return
                SelectedBlockKind != null &&
                SelectedModuleKind != null &&
                SelectedModuleKind != null &&
                Channels.Any(ch => ch.IsSelected);
        }

        public ModuleSelectorModel DeepClone()
        {
            return
                new ModuleSelectorModel(MainViewModel.Index)
                {
                    SelectedBlockKind = SelectedBlockKind,
                    SelectedModuleKind = SelectedModuleKind,
                    SelectedModificationKind = SelectedModificationKind
                };
        }

        public IEnumerable<ComponentTarget> GetTargets(bool SelectedOnly = true)
        {
            return
                Channels.Where(c => !SelectedOnly || c.IsSelected).Select(channel =>
                    new ComponentTarget()
                    {
                        CellId = SelectedBlockKind.Id,
                        CellModification = SelectedModificationKind.Id,
                        Module = SelectedModuleKind.Id,
                        Channel = channel.Id
                    }).ToList();
        }

        private void OnSelectionChanged()
        {
            if (SelectionChanged != null)
                SelectionChanged(this, new ModuleSelectedEventArgs());
        }
        public event EventHandler<ModuleSelectedEventArgs> SelectionChanged;
        public class ModuleSelectedEventArgs : EventArgs
        {
        }
    }
}
