using FirmwarePacker.LaunchParameters;
using FirmwarePacker.RecentProjects;
using FirmwarePacking.Annotations;
using Microsoft.Practices.Prism.Events;

namespace FirmwarePacker.ViewModels.Factories
{
    [UsedImplicitly]
    public class RootViewModelFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILaunchParameters _launchParameters;
        private readonly ILoadProjectService _loadProjectService;
        private readonly MainViewModelFactory _mainViewModelFactory;
        private readonly IPackageSavingService _packageSavingService;
        private readonly IRecentProjectsService _recentProjectsService;
        private readonly SelectProjectViewModelFactory _selectProjectViewModelFactory;

        public RootViewModelFactory(MainViewModelFactory MainViewModelFactory, SelectProjectViewModelFactory SelectProjectViewModelFactory,
                                    IEventAggregator EventAggregator, ILaunchParameters LaunchParameters, ILoadProjectService LoadProjectService,
                                    IRecentProjectsService RecentProjectsService, IPackageSavingService PackageSavingService)
        {
            _mainViewModelFactory = MainViewModelFactory;
            _eventAggregator = EventAggregator;
            _selectProjectViewModelFactory = SelectProjectViewModelFactory;
            _launchParameters = LaunchParameters;
            _loadProjectService = LoadProjectService;
            _recentProjectsService = RecentProjectsService;
            _packageSavingService = PackageSavingService;
        }

        public RootViewModel GetViewModel()
        {
            return new RootViewModel(_mainViewModelFactory, _selectProjectViewModelFactory, _eventAggregator, _launchParameters, _loadProjectService,
                                     _recentProjectsService, _packageSavingService);
        }
    }
}
