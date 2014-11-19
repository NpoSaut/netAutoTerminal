using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.FirmwareSources;

namespace FirmwareBurner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(RepoFirmwareSource AutoFirmwareSource, ManualFirmwareSource UserFirmwareSource, ProjectViewModel Project)
        {
            this.Project = Project;
            this.AutoFirmwareSource = AutoFirmwareSource;
            this.UserFirmwareSource = UserFirmwareSource;
        }

        public ProjectViewModel Project { get; private set; }

        public RepoFirmwareSource AutoFirmwareSource { get; private set; }
        public ManualFirmwareSource UserFirmwareSource { get; private set; }
    }
}

