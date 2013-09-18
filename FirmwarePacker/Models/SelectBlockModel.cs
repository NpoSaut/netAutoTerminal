﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwarePacking.SystemsIndexes;

namespace FirmwarePacker.Models
{
    public class ModuleSelectorModel : ViewModel, IDataCheck
    {
        private List<SystemKind> _SystemKinds;
        public List<SystemKind> SystemKinds
        {
            get { return _SystemKinds; }
        }

        private SystemKind _SelectedSystemKind;
        public SystemKind SelectedSystemKind
        {
            get { return _SelectedSystemKind; }
            set
            {
                if (_SelectedSystemKind != value)
                {
                    var oldValue = _SelectedSystemKind;
                    _SelectedSystemKind = value;
                    OnSystemKindChanged(oldValue, value);
                }
            }
        }
        private void OnSystemKindChanged(SystemKind oldValue, SystemKind value)
        {
            OnPropertyChanged("SelectedSystemKind");
            BlockKinds = value.Blocks;
        }

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
            Channels = Enumerable.Range(1, value.ChannelsCount).Select(i => new ChannelModel(i)).ToList();
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
        }

        private int _Modification;
        public int Modification
        {
            get { return _Modification; }
            set
            {
                if (_Modification != value)
                {
                    _Modification = value;
                    OnPropertyChanged("Modification");
                }
            }
        }

        public ModuleSelectorModel(Index index)
        {
            this._SystemKinds = index.Systems.ToList();
            SelectedSystemKind = SystemKinds.FirstOrDefault();
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

        public class ChannelModel : ViewModel
        {
            public int Id { get; private set; }

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
                    }
                }
            }

            public ChannelModel(int Id, bool IsSelected)
            {
                this.Id = Id;
                this.IsSelected = IsSelected;
            }
            public ChannelModel(int Id)
                : this(Id, true)
            { }

            public override string ToString()
            {
                return string.Format("Канал {0}", Id);
            }
        }

        public bool Check()
        {
            return
                SelectedSystemKind != null &&
                SelectedBlockKind != null &&
                SelectedModuleKind != null &&
                Modification >= 0 &&
                Channels.Any(ch => ch.IsSelected);
        }

        public ModuleSelectorModel DeepClone()
        {
            return
                new ModuleSelectorModel(MainModel.Index)
                {
                    SelectedSystemKind = SelectedSystemKind,
                    SelectedBlockKind = SelectedBlockKind,
                    SelectedModuleKind = SelectedModuleKind,
                    Modification = Modification
                };
        }
    }
}
