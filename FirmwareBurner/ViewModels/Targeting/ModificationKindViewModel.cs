using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace FirmwareBurner.ViewModels.Targeting
{
    /// <summary>Модель представления для модификации ячейки</summary>
    public class ModificationKindViewModel
    {
        public ModificationKindViewModel(int Id, string Name, string DeviceName, bool CanBeBurned)
        {
            this.CanBeBurned = CanBeBurned;
            this.DeviceName = DeviceName;
            this.Id = Id;
            this.Name = Name;

            SelectCommand = new DelegateCommand(SelectMe, CanSelectMe);
        }

        /// <summary>Номер модификации ячейки</summary>
        public int Id { get; private set; }

        /// <summary>Назчание модификации</summary>
        public String Name { get; private set; }

        /// <summary>Название устройства, на котором основана ячейка</summary>
        public String DeviceName { get; private set; }

        /// <summary>Показывает, имеется ли в программе инструментарий, способный прошить данную модификацию</summary>
        public Boolean CanBeBurned { get; private set; }

        public ICommand SelectCommand { get; private set; }
        private bool CanSelectMe() { return CanBeBurned; }
        private void SelectMe() { OnTargetSelected(); }

        public event EventHandler TargetSelected;

        protected virtual void OnTargetSelected()
        {
            EventHandler handler = TargetSelected;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
