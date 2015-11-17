using FirmwarePacker.RecentProjects;
using FirmwarePacking.Annotations;

namespace FirmwarePacker.ViewModels.Factories
{
    [UsedImplicitly]
    public class MainViewModelFactory
    {
        private readonly IPackageSavingService _packageSavingService;
        private readonly IRecentProjectsService _recentProjectsService;

        public MainViewModelFactory(IPackageSavingService PackageSavingService, IRecentProjectsService RecentProjectsService)
        {
            _packageSavingService = PackageSavingService;
            _recentProjectsService = RecentProjectsService;
        }

        public MainViewModel GetInstance(FirmwareVersionViewModel Version, ProjectViewModel Project)
        {
            return new MainViewModel(Version, Project, _packageSavingService, _recentProjectsService);
        }
    }
}
