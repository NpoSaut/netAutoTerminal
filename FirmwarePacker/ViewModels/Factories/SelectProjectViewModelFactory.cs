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
                                              //_recentProjectsService.GetRecentProjects().Select(p => _recentProjectViewModelFactory.GetViewModel(p)).ToList());
                                              new[]
                                              {
                                                  new RecentProjectViewModel("Project 1.fpc",
                                                                             @"C:\Users\plyusnin\Sources\netFirmwaring\FirmwarePacker\bin\Debug", "", "1.3",
                                                                             null, null),
                                                  new RecentProjectViewModel("Project 2.fpc",
                                                                             @"C:\Users\plyusnin\Sources", "", "1.3",
                                                                             null, null),
                                                  new RecentProjectViewModel("Project 3.fpc",
                                                                             @"C:\Users\plyusnin\Sources\netFirmwaring\FirmwarePackerGagagaga\bin\Debug", "", "1.3",
                                                                             null, null)
                                              });
        }
    }
}
