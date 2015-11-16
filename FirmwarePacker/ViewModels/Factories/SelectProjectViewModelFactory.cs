using FirmwarePacking.Annotations;
using Microsoft.Practices.Prism.Events;

namespace FirmwarePacker.ViewModels.Factories
{
    [UsedImplicitly]
    public class SelectProjectViewModelFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoadProjectService _loadProjectService;

        public SelectProjectViewModelFactory(IEventAggregator EventAggregator, ILoadProjectService LoadProjectService)
        {
            _eventAggregator = EventAggregator;
            _loadProjectService = LoadProjectService;
        }

        public SelectProjectViewModel GetViewModel() { return new SelectProjectViewModel(_loadProjectService, _eventAggregator); }
    }
}
