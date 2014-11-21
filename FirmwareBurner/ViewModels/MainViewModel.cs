using FirmwareBurner.Events;
using FirmwareBurner.Models.Project;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //private readonly IBurnManager _burnManager;
        private readonly IBurningViewModelProvider _burningViewModelProvider;
        private readonly IFirmwareProjectFactory _firmwareProjectFactory;
        private readonly IProjectViewModelProvider _projectViewModelProvider;

        public MainViewModel(TargetSelectorViewModel TargetSelector, IEventAggregator EventAggregator, IProjectViewModelProvider ProjectViewModelProvider,
                             IBurningViewModelProvider BurningViewModelProvider, IFirmwareProjectFactory FirmwareProjectFactory)
        {
            this.TargetSelector = TargetSelector;
            _projectViewModelProvider = ProjectViewModelProvider;
            _burningViewModelProvider = BurningViewModelProvider;
            _firmwareProjectFactory = FirmwareProjectFactory;

            EventAggregator.GetEvent<TargetSelectedEvent>().Subscribe(OnTargetSelected);
        }

        public TargetSelectorViewModel TargetSelector { get; private set; }
        public ProjectViewModel Project { get; private set; }
        public BurningViewModel Burning { get; private set; }

        private void OnTargetSelected(TargetSelectedArgs SelectedArgs)
        {
            Project = _projectViewModelProvider.GetViewModel(SelectedArgs.CellKindId, SelectedArgs.ModificationId);
            Burning = _burningViewModelProvider.GetViewModel(SelectedArgs.CellKindId, SelectedArgs.ModificationId,
                                                             new ViewModelProjectAssembler(Project, _firmwareProjectFactory));

            RaisePropertyChanged("Project");
            RaisePropertyChanged("Burning");
        }
    }
}
