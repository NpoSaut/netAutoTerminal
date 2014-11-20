using System.Collections.Generic;
using FirmwareBurner.Events;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Tools;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.Targeting
{
    /// <summary>Модель представления выбора цели прошивки</summary>
    public class TargetSelectorViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private CellKindViewModel _selectedCellKind;
        private ChannelViewModel _selectedChannel;
        private ModificationKindViewModel _selectedModificationKind;

        public TargetSelectorViewModel(ICellsCatalogProvider CellsCatalogProvider, IEventAggregator EventAggregator)
        {
            _eventAggregator = EventAggregator;
            CellKinds = CellsCatalogProvider.GetCatalog();
        }

        /// <summary>Известные типы ячеек</summary>
        public IList<CellKindViewModel> CellKinds { get; private set; }

        public CellKindViewModel SelectedCellKind
        {
            get { return _selectedCellKind; }
            set
            {
                if (_selectedCellKind != value)
                {
                    _selectedCellKind = value;
                    RaisePropertyChanged("SelectedCellKind");
                    OnTargetChanged();
                }
            }
        }

        public ModificationKindViewModel SelectedModificationKind
        {
            get { return _selectedModificationKind; }
            set
            {
                if (_selectedModificationKind != value)
                {
                    _selectedModificationKind = value;
                    RaisePropertyChanged("SelectedModificationKind");
                    OnTargetChanged();
                }
            }
        }

        public ChannelViewModel SelectedChannel
        {
            get { return _selectedChannel; }
            set
            {
                if (_selectedChannel != value)
                {
                    _selectedChannel = value;
                    RaisePropertyChanged("SelectedChannel");
                    OnTargetChanged();
                }
            }
        }

        protected virtual void OnTargetChanged()
        {
            if (SelectedCellKind != null && SelectedModificationKind != null)
            {
                _eventAggregator.GetEvent<TargetSelectedEvent>().Publish(
                    new TargetSelectedArgs(SelectedCellKind.Id, SelectedModificationKind.Id));
            }
        }
    }
}
