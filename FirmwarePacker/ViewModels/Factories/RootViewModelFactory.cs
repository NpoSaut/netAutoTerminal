using FirmwarePacker.LaunchParameters;
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
        private readonly SelectProjectViewModelFactory _selectProjectViewModelFactory;

        public RootViewModelFactory(MainViewModelFactory MainViewModelFactory, SelectProjectViewModelFactory SelectProjectViewModelFactory,
                                    IEventAggregator EventAggregator, ILaunchParameters LaunchParameters, ILoadProjectService LoadProjectService)
        {
            _mainViewModelFactory = MainViewModelFactory;
            _eventAggregator = EventAggregator;
            _selectProjectViewModelFactory = SelectProjectViewModelFactory;
            _launchParameters = LaunchParameters;
            _loadProjectService = LoadProjectService;
        }

        public RootViewModel GetViewModel()
        {
            return new RootViewModel(_mainViewModelFactory, _selectProjectViewModelFactory, _eventAggregator, _launchParameters, _loadProjectService);
        }
    }
}
