using FirmwareBurner.Events;
using FirmwareBurner.Project;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //private readonly IBurningReceipt _burnManager;
        private readonly IBurningViewModelProvider _burningViewModelProvider;
        private readonly IFirmwareProjectFactory _firmwareProjectFactory;
        private readonly IProjectViewModelProvider _projectViewModelProvider;
        private readonly IProjectValidatorViewModelProvider _validatorViewModelProvider;

        public MainViewModel(TargetSelectorViewModel TargetSelector, IEventAggregator EventAggregator, IProjectViewModelProvider ProjectViewModelProvider,
                             IBurningViewModelProvider BurningViewModelProvider, IFirmwareProjectFactory FirmwareProjectFactory,
                             IProjectValidatorViewModelProvider ValidatorViewModelProvider, FileRequestServiceViewModel FileRequestServiceViewModel)
        {
            this.TargetSelector = TargetSelector;
            _projectViewModelProvider = ProjectViewModelProvider;
            _burningViewModelProvider = BurningViewModelProvider;
            _firmwareProjectFactory = FirmwareProjectFactory;
            _validatorViewModelProvider = ValidatorViewModelProvider;
            this.FileRequestServiceViewModel = FileRequestServiceViewModel;

            EventAggregator.GetEvent<TargetSelectedEvent>().Subscribe(OnTargetSelected);
        }

        public FileRequestServiceViewModel FileRequestServiceViewModel { get; private set; }
        public TargetSelectorViewModel TargetSelector { get; private set; }
        public ProjectViewModel Project { get; private set; }
        public BurningViewModel Burning { get; private set; }
        public ProjectValidatorViewModel Validator { get; private set; }

        private void OnTargetSelected(TargetSelectedArgs SelectedArgs)
        {
            Project = _projectViewModelProvider.GetViewModel(SelectedArgs.CellKindId, SelectedArgs.ModificationId);
            Burning = _burningViewModelProvider.GetViewModel(SelectedArgs.CellKindId, SelectedArgs.ModificationId,
                                                             new ViewModelProjectAssembler(Project, _firmwareProjectFactory));
            Validator = _validatorViewModelProvider.GetViewModel(Project);

            RaisePropertyChanged("Project");
            RaisePropertyChanged("Burning");
            RaisePropertyChanged("Validator");
        }
    }
}
