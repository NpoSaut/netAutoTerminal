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
}
