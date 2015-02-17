using FirmwareBurner.Project;

namespace FirmwareBurner.ViewModels
{
    public class ProjectManagerViewModelFactory
    {
        private readonly IBurningViewModelFactory _burningViewModelFactory;
        private readonly IFirmwareProjectFactory _firmwareProjectFactory;
        private readonly IProjectViewModelProvider _projectViewModelProvider;
        private readonly IProjectValidatorViewModelProvider _validatorViewModelProvider;

        public ProjectManagerViewModelFactory(IBurningViewModelFactory BurningViewModelFactory, IFirmwareProjectFactory FirmwareProjectFactory,
                                              IProjectViewModelProvider ProjectViewModelProvider, IProjectValidatorViewModelProvider ValidatorViewModelProvider)
        {
            _burningViewModelFactory = BurningViewModelFactory;
            _firmwareProjectFactory = FirmwareProjectFactory;
            _projectViewModelProvider = ProjectViewModelProvider;
            _validatorViewModelProvider = ValidatorViewModelProvider;
        }

        public ProjectManagerViewModel GetViewModel(int CellKindId, int ModificationId)
        {
            ProjectViewModel project = _projectViewModelProvider.GetViewModel(CellKindId, ModificationId);
            BurningViewModel burning = _burningViewModelFactory.GetViewModel(CellKindId, ModificationId,
                                                                              new ViewModelProjectAssembler(project, _firmwareProjectFactory));
            ProjectValidatorViewModel validator = _validatorViewModelProvider.GetViewModel(project);
            return new ProjectManagerViewModel(project, burning, validator);
        }
    }
}