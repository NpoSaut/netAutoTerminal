using FirmwareBurner.Project;
using FirmwareBurner.Validation;

namespace FirmwareBurner.ViewModels
{
    public class ProjectManagerViewModelFactory
    {
        private readonly IBurningViewModelFactory _burningViewModelFactory;
        private readonly IFirmwareProjectFactory _firmwareProjectFactory;
        private readonly IProjectViewModelProvider _projectViewModelProvider;
        private readonly IValidationContextFactory _validationContextFactory;

        public ProjectManagerViewModelFactory(IBurningViewModelFactory BurningViewModelFactory, IFirmwareProjectFactory FirmwareProjectFactory,
                                              IProjectViewModelProvider ProjectViewModelProvider, IValidationContextFactory ValidationContextFactory)
        {
            _burningViewModelFactory = BurningViewModelFactory;
            _firmwareProjectFactory = FirmwareProjectFactory;
            _projectViewModelProvider = ProjectViewModelProvider;
            _validationContextFactory = ValidationContextFactory;
        }

        public ProjectManagerViewModel GetViewModel(int CellKindId, int ModificationId)
        {
            IValidationContext validationContext = _validationContextFactory.GetValidationContext();
            ProjectViewModel project = _projectViewModelProvider.GetViewModel(CellKindId, ModificationId, validationContext);
            var projectAssembler = new ViewModelProjectAssembler(project, _firmwareProjectFactory);
            BurningViewModel burning = _burningViewModelFactory.GetViewModel(CellKindId, ModificationId,
                                                                             validationContext, projectAssembler);
            return new ProjectManagerViewModel(project, burning);
        }
    }
}
