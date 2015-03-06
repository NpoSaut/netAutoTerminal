using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace FirmwareBurner.ViewModels.Targeting
{
    /// <summary>Модель представления для модификации ячейки</summary>
    public class ModificationKindViewModel
    {
        public ModificationKindViewModel(int Id, string Name, string DeviceName)
        {
            this.DeviceName = DeviceName;
            this.Id = Id;
            this.Name = Name;

            SelectCommand = new DelegateCommand(SelectMe);
        }

        /// <summary>Номер модификации ячейки</summary>
        public int Id { get; private set; }

        /// <summary>Назчание модификации</summary>
        public String Name { get; private set; }

        /// <summary>Название устройства, на котором основана ячейка</summary>
        public String DeviceName { get; private set; }

        public ICommand SelectCommand { get; private set; }
        private void SelectMe() { OnTargetSelected(); }

        public event EventHandler TargetSelected;

        protected virtual void OnTargetSelected()
        {
            EventHandler handler = TargetSelected;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
