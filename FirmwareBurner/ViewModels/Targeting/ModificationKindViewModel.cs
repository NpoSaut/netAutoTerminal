using System;

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
        }

        /// <summary>Номер модификации ячейки</summary>
        public int Id { get; private set; }

        /// <summary>Назчание модификации</summary>
        public String Name { get; private set; }

        /// <summary>Название устройства, на котором основана ячейка</summary>
        public String DeviceName { get; private set; }
    }
}
