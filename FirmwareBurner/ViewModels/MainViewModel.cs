using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FirmwareBurner.Events;
using FirmwareBurner.Models.Project;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //private readonly IBurnManager _burnManager;
        private readonly IFirmwareProjectFactory _firmwareProjectFactory;
        private readonly IProjectViewModelProvider _projectViewModelProvider;
        private ChannelViewModel _selectedChannel;

        public MainViewModel(TargetSelectorViewModel TargetSelector, IEventAggregator EventAggregator, IProjectViewModelProvider ProjectViewModelProvider,
                             IFirmwareProjectFactory FirmwareProjectFactory)
        {
            this.TargetSelector = TargetSelector;
            _projectViewModelProvider = ProjectViewModelProvider;
            _firmwareProjectFactory = FirmwareProjectFactory;

            EventAggregator.GetEvent<TargetSelectedEvent>().Subscribe(OnTargetSelected);

            BurnCommand = new DelegateCommand(Burn);
        }

        public TargetSelectorViewModel TargetSelector { get; private set; }
        public ProjectViewModel Project { get; private set; }
        public ICommand BurnCommand { get; private set; }

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

        private void OnTargetSelected(TargetSelectedArgs SelectedArgs)
        {
            Project = _projectViewModelProvider.GetViewModel(SelectedArgs.CellKindId, SelectedArgs.ModificationId);
            Channels = Enumerable.Range(0, SelectedArgs.ChannelsCount).Select(i => new ChannelViewModel(i + 1)).ToList();
            SelectedChannel = Channels.FirstOrDefault();
            RaisePropertyChanged("Project");
            RaisePropertyChanged("Channels");
            RaisePropertyChanged("SelectedChannel");
        }

        private void Burn()
        {
            FirmwareProject project = _firmwareProjectFactory.GetProject(Project.CellKindId, Project.CellModificationId, SelectedChannel.Number,
                                                                         Project.BlockDetails.SerialNumber,
                                                                         Project.BlockDetails.AssemblyDate,
                                                                         Project.FirmwareSetConstructor.Components.Select(
                                                                             c => Tuple.Create(c.ModuleIndex, c.FirmwareSelector.SelectedPackage)).ToList());
            //_burnManager.Burn(project);
        }
    }
}
