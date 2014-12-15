using FirmwareBurner.Project;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels
{
    public class ProjectManagerViewModel : ViewModelBase
    {
        public ProjectManagerViewModel(ProjectViewModel Project, BurningViewModel Burning, ProjectValidatorViewModel Validator)
        {
            this.Project = Project;
            this.Burning = Burning;
            this.Validator = Validator;
        }

        public ProjectViewModel Project { get; private set; }
        public BurningViewModel Burning { get; private set; }
        public ProjectValidatorViewModel Validator { get; private set; }
    }

    public class ProjectManagerViewModelFactory
    {
        private readonly IBurningViewModelProvider _burningViewModelProvider;
        private readonly IFirmwareProjectFactory _firmwareProjectFactory;
        private readonly IProjectViewModelProvider _projectViewModelProvider;
        private readonly IProjectValidatorViewModelProvider _validatorViewModelProvider;

        public ProjectManagerViewModelFactory(IBurningViewModelProvider BurningViewModelProvider, IFirmwareProjectFactory FirmwareProjectFactory,
                                              IProjectViewModelProvider ProjectViewModelProvider, IProjectValidatorViewModelProvider ValidatorViewModelProvider)
        {
            _burningViewModelProvider = BurningViewModelProvider;
            _firmwareProjectFactory = FirmwareProjectFactory;
            _projectViewModelProvider = ProjectViewModelProvider;
            _validatorViewModelProvider = ValidatorViewModelProvider;
        }

        public ProjectManagerViewModel GetViewModel(int CellKindId, int ModificationId)
        {
            ProjectViewModel project = _projectViewModelProvider.GetViewModel(CellKindId, ModificationId);
            BurningViewModel burning = _burningViewModelProvider.GetViewModel(CellKindId, ModificationId,
                                                                              new ViewModelProjectAssembler(project, _firmwareProjectFactory));
            ProjectValidatorViewModel validator = _validatorViewModelProvider.GetViewModel(project);
            return new ProjectManagerViewModel(project, burning, validator);
        }
    }
}
