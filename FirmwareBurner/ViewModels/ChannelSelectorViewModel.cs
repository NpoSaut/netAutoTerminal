using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;

namespace FirmwareBurner.ViewModels
{
    public class ChannelSelectorViewModel : ViewModelBase, IChannelSelector
    {
        private ChannelViewModel _selectedChannel;

        public ChannelSelectorViewModel(int ChannelsCount)
        {
            Channels = Enumerable.Range(0, ChannelsCount).Select(i => new ChannelViewModel(i + 1)).ToList();
            SelectedChannel = Channels.FirstOrDefault();
        }

        public IList<ChannelViewModel> Channels { get; private set; }

        public ChannelViewModel SelectedChannel
        {
            get { return _selectedChannel; }
            set
            {
                if (Equals(value, _selectedChannel)) return;
                _selectedChannel = value;
                RaisePropertyChanged("SelectedChannel");
            }
        }

        int IChannelSelector.SelectedChannelNumber
        {
            get { return SelectedChannel.Number; }
        }
    }
}
