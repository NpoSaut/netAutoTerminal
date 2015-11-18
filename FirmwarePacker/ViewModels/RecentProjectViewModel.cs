using System;
using System.IO;
using System.Windows.Input;
using FirmwarePacker.Events;
using FirmwarePacker.LoadingServices;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace FirmwarePacker.ViewModels
{
    public class RecentProjectViewModel : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly string _filePath;
        private readonly ILoadProjectService _loadProjectService;

        public RecentProjectViewModel(string Name, string Directory, string FilePath, string LastVersion, IEventAggregator EventAggregator,
                                      ILoadProjectService LoadProjectService)
        {
            this.Name = Name;
            this.Directory = Directory;
            this.LastVersion = LastVersion;
            _filePath = FilePath;
            _eventAggregator = EventAggregator;
            _loadProjectService = LoadProjectService;

            LoadCommand = new DelegateCommand(Load, CanLoad);
        }

        public String Name { get; private set; }
        public String Directory { get; private set; }
        public String LastVersion { get; private set; }
        public ICommand LoadCommand { get; private set; }

        private bool CanLoad() { return File.Exists(_filePath); }

        private void Load()
        {
            _eventAggregator.GetEvent<ProjectLoadedEvent>().Publish(new ProjectLoadedEvent.Payload(_loadProjectService.LoadProject(_filePath)));
        }
    }
}
