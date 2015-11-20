using System.Windows.Input;
using FirmwarePacker.LoadingServices;
using FirmwarePacker.Project;
using FirmwarePacker.RecentProjects;
using FirmwarePacker.TriggerActions.Notifications;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly IPackageSavingService _packageSavingService;
        private readonly IRecentProjectsService _recentProjectsService;

        public MainViewModel(FirmwareVersionViewModel Version, ProjectViewModel Project, IPackageSavingService PackageSavingService,
                             IRecentProjectsService RecentProjectsService)
        {
            this.Project = Project;
            _packageSavingService = PackageSavingService;
            _recentProjectsService = RecentProjectsService;
            this.Version = Version;
            SaveCommand = new DelegateCommand(Save, Verify);
            SaveFileRequest = new InteractionRequest<SaveFileInteractionContext>();
        }

        public FirmwareVersionViewModel Version { get; private set; }
        public ProjectViewModel Project { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public InteractionRequest<SaveFileInteractionContext> SaveFileRequest { get; private set; }

        private bool Verify() { return Project.Check(); }

        private void Save()
        {
            PackageVersion version = Version.GetModel();
            _packageSavingService.SavePackage(Project.GetModel(), version, Project.ProjectRoot);

            _recentProjectsService.UpdateRecentProject(
                new RecentProject { FileName = Project.FilePath, MajorVersion = version.Major, MinorVersion = version.Minor });
        }
    }
}
