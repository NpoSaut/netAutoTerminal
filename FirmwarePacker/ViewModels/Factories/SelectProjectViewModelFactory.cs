using System.Linq;
using FirmwarePacker.LoadingServices;
using FirmwarePacker.RecentProjects;
using FirmwarePacking.Annotations;
using Microsoft.Practices.Prism.Events;

namespace FirmwarePacker.ViewModels.Factories
{
    [UsedImplicitly]
    public class SelectProjectViewModelFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoadProjectService _loadProjectService;
        private readonly RecentProjectViewModelFactory _recentProjectViewModelFactory;
        private readonly IRecentProjectsService _recentProjectsService;

        public SelectProjectViewModelFactory(IEventAggregator EventAggregator, ILoadProjectService LoadProjectService,
                                             IRecentProjectsService RecentProjectsService, RecentProjectViewModelFactory RecentProjectViewModelFactory)
        {
            _eventAggregator = EventAggregator;
            _loadProjectService = LoadProjectService;
            _recentProjectsService = RecentProjectsService;
            _recentProjectViewModelFactory = RecentProjectViewModelFactory;
        }

        public SelectProjectViewModel GetViewModel()
        {
            return new SelectProjectViewModel(_loadProjectService, _eventAggregator,
                                              _recentProjectsService.GetRecentProjects().Select(p => _recentProjectViewModelFactory.GetViewModel(p)).ToList());
        }
    }
}
