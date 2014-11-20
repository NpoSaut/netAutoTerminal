using FirmwareBurner.Events;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IProjectViewModelProvider _projectViewModelProvider;

        public MainViewModel(TargetSelectorViewModel TargetSelector, IEventAggregator EventAggregator, IProjectViewModelProvider ProjectViewModelProvider)
        {
            this.TargetSelector = TargetSelector;
            _projectViewModelProvider = ProjectViewModelProvider;

            EventAggregator.GetEvent<TargetSelectedEvent>().Subscribe(OnTargetSelected);
        }

        public TargetSelectorViewModel TargetSelector { get; private set; }
        public ProjectViewModel Project { get; private set; }

        private void OnTargetSelected(TargetSelectedArgs SelectedArgs)
        {
            Project = _projectViewModelProvider.GetViewModel(SelectedArgs.CellKindId, SelectedArgs.ModificationId);
            RaisePropertyChanged("Project");
        }
    }
}
