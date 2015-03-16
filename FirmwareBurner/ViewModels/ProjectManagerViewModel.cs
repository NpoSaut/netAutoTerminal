using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels
{
    public class ProjectManagerViewModel : ViewModelBase
    {
        public ProjectManagerViewModel(ProjectViewModel Project, BurningViewModel Burning)
        {
            this.Project = Project;
            this.Burning = Burning;
        }

        public ProjectViewModel Project { get; private set; }
        public BurningViewModel Burning { get; private set; }
    }
}
