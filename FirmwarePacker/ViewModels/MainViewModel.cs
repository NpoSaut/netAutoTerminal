using System.Windows.Input;
using FirmwarePacker.TriggerActions.Notifications;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly IPackageSavingService _packageSavingService;

        public MainViewModel(FirmwareVersionViewModel Version, ProjectViewModel Project, IPackageSavingService PackageSavingService)
        {
            this.Project = Project;
            _packageSavingService = PackageSavingService;
            this.Version = Version;
            SaveCommand = new DelegateCommand(Save, Verify);
            SaveFileRequest = new InteractionRequest<SaveFileInteractionContext>();
        }

        public FirmwareVersionViewModel Version { get; private set; }
        public ProjectViewModel Project { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public InteractionRequest<SaveFileInteractionContext> SaveFileRequest { get; private set; }

        private bool Verify() { return Project.Check(); }
        private void Save() { _packageSavingService.SavePackage(SaveFileRequest, Project.GetModel(), Version.GetModel(), Project.ProjectRoot); }
    }
}
