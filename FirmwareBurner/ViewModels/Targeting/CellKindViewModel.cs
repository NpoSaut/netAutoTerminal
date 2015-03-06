using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FirmwareBurner.ViewModels.Bases;
using Microsoft.Practices.Prism.Commands;

namespace FirmwareBurner.ViewModels.Targeting
{
    /// <summary>Модель представления для типа ячейки</summary>
    public class CellKindViewModel : ViewModelBase
    {
        public CellKindViewModel(int Id, string Name, IList<ModificationKindViewModel> Modifications, IList<ChannelViewModel> Channels)
        {
            this.Id = Id;
            this.Name = Name;
            this.Modifications = Modifications;
            this.Channels = Channels;

            foreach (ModificationKindViewModel modification in Modifications)
                modification.TargetSelected += ModificationOnTargetSelected;

            SelectCommand = new DelegateCommand(SelectMe);
        }

        /// <summary>Идентификатор типа ячейки</summary>
        public Int32 Id { get; private set; }

        /// <summary>Название типа ячейки</summary>
        public String Name { get; private set; }

        /// <summary>Список модификаций ячеек данного типа</summary>
        public IList<ModificationKindViewModel> Modifications { get; private set; }

        /// <summary>Список каналов устройства</summary>
        public IList<ChannelViewModel> Channels { get; private set; }

        public ICommand SelectCommand { get; private set; }

        private void ModificationOnTargetSelected(object Sender, EventArgs e)
        {
            OnTargetSelected(new TargetSelectedEventArgs(this, (ModificationKindViewModel)Sender));
        }

        private void SelectMe() { OnTargetSelected(new TargetSelectedEventArgs(this, Modifications.First())); }

        public event EventHandler<TargetSelectedEventArgs> TargetSelected;

        protected virtual void OnTargetSelected(TargetSelectedEventArgs E)
        {
            EventHandler<TargetSelectedEventArgs> handler = TargetSelected;
            if (handler != null) handler(this, E);
        }
    }

    public class TargetSelectedEventArgs : EventArgs
    {
        public TargetSelectedEventArgs(CellKindViewModel Cell, ModificationKindViewModel Modification)
        {
            this.Cell = Cell;
            this.Modification = Modification;
        }

        public CellKindViewModel Cell { get; private set; }
        public ModificationKindViewModel Modification { get; private set; }
    }
}
