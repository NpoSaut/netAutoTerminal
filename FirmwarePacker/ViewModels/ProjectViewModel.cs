using System.Collections.Generic;
using System.Windows.Input;
using FirmwarePacker.Events;
using FirmwarePacker.Project;
using FirmwarePacker.TriggerActions.Notifications;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker.ViewModels
{
    public class ProjectViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoadProjectService _loadProjectService;
        private readonly PackageProject _project;

        public ProjectViewModel(PackageProject Project, string Name, string FilePath,
                                string ProjectRoot, ICollection<TargetViewModel> Targets, ILoadProjectService LoadProjectService, IEventAggregator EventAggregator)
        {
            _loadProjectService = LoadProjectService;
            _eventAggregator = EventAggregator;
            this.Name = Name;
            _project = Project;
            this.FilePath = FilePath;
            this.Targets = Targets;
            this.ProjectRoot = ProjectRoot;
            LoadProjectCommand = new DelegateCommand(LoadProject);
            OpenFileRequest = new InteractionRequest<OpenFileInteractionContext>();
        }

        public string FilePath { get; private set; }
        public string Name { get; private set; }
        public ICollection<TargetViewModel> Targets { get; private set; }

        public ICommand LoadProjectCommand { get; private set; }

        public InteractionRequest<OpenFileInteractionContext> OpenFileRequest { get; private set; }

        public string ProjectRoot { get; private set; }

        private void LoadProject()
        {
            _loadProjectService.RequestLoadProject(OpenFileRequest,
                                                   p => _eventAggregator.GetEvent<ProjectLoadedEvent>().Publish(new ProjectLoadedEvent.Payload(p)));
        }

        public bool Check() { return true; }
        public PackageProject GetModel() { return _project; }
    }
}
