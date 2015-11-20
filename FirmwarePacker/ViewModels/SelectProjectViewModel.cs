using System.Collections.Generic;
using System.Windows.Input;
using FirmwarePacker.Events;
using FirmwarePacker.LoadingServices;
using FirmwarePacker.TriggerActions.Notifications;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker.ViewModels
{
    public class SelectProjectViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoadProjectService _loadProjectService;

        public SelectProjectViewModel(ILoadProjectService LoadProjectService, IEventAggregator EventAggregator, IList<RecentProjectViewModel> RecentProjects)
        {
            this.RecentProjects = RecentProjects;
            _loadProjectService = LoadProjectService;
            _eventAggregator = EventAggregator;
            OpenFileRequest = new InteractionRequest<OpenFileInteractionContext>();
            LoadProjectCommand = new DelegateCommand(Load);
        }

        public IList<RecentProjectViewModel> RecentProjects { get; private set; }
        public ICommand LoadProjectCommand { get; private set; }

        public InteractionRequest<OpenFileInteractionContext> OpenFileRequest { get; private set; }
        private void Load() { _loadProjectService.RequestLoadProject(PackageLoaded); }

        private void PackageLoaded(ProjectViewModel ProjectViewModel)
        {
            _eventAggregator.GetEvent<ProjectLoadedEvent>().Publish(new ProjectLoadedEvent.Payload(ProjectViewModel));
        }
    }
}
