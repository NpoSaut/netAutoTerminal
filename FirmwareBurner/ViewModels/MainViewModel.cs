using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;

namespace FirmwareBurner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(ProjectViewModel Project, TargetSelectorViewModel TargetSelector)
        {
            this.TargetSelector = TargetSelector;
            this.Project = Project;
        }

        public TargetSelectorViewModel TargetSelector { get; private set; }

        public ProjectViewModel Project { get; private set; }
    }
}
