using System;
using System.Linq;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels.Targeting
{
    /// <summary>Модель представления выбора цели прошивки</summary>
    public class TargetSelectorViewModel : ViewModelBase
    {
        private CellKindViewModel _selectedCellKind;
        private ChannelViewModel _selectedChannel;
        private ModificationKindViewModel _selectedModificationKind;

        public CellKindViewModel SelectedCellKind
        {
            get { return _selectedCellKind; }
            set
            {
                if (_selectedCellKind != value)
                {
                    _selectedCellKind = value;
                    OnPropertyChanged("SelectedCellKind");
                    OnSelectedCellKindChanged();
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
                    OnPropertyChanged("SelectedModificationKind");
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
                    OnPropertyChanged("SelectedChannel");
                }
            }
        }

        public event EventHandler TargetChanged;

        private void OnSelectedCellKindChanged()
        {
            if (SelectedCellKind.Modifications.Count == 1)
                SelectedModificationKind = SelectedCellKind.Modifications.First();
            SelectedChannel = SelectedCellKind.Channels.FirstOrDefault();
        }

        protected virtual void OnTargetChanged()
        {
            EventHandler handler = TargetChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
