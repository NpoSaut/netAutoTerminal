using System;
using System.Collections.Generic;
using FirmwareBurner.ViewModels.Bases;

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
}
