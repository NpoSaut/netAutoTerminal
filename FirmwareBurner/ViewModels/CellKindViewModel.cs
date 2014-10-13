using System;
using System.Collections.Generic;

namespace FirmwareBurner.ViewModels
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
        }

        /// <summary>Идентификатор типа ячейки</summary>
        public Int32 Id { get; private set; }

        /// <summary>Название типа ячейки</summary>
        public String Name { get; private set; }

        /// <summary>Список модификаций ячеек данного типа</summary>
        public IList<ModificationKindViewModel> Modifications { get; private set; }

        /// <summary>Список каналов устройства</summary>
        public IList<ChannelViewModel> Channels { get; private set; }
    }

    /// <summary>Модель представления для модификации ячейки</summary>
    public class ModificationKindViewModel
    {
        public ModificationKindViewModel(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        /// <summary>Номер модификации ячейки</summary>
        public int Id { get; private set; }

        /// <summary>Назчание модификации</summary>
        public String Name { get; private set; }
    }
}
