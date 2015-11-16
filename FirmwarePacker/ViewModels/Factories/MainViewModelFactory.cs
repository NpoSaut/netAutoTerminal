using FirmwarePacking.Annotations;

namespace FirmwarePacker.ViewModels.Factories
{
    [UsedImplicitly]
    public class MainViewModelFactory
    {
        private readonly IPackageSavingService _packageSavingService;

        public MainViewModelFactory(IPackageSavingService PackageSavingService) { _packageSavingService = PackageSavingService; }

        public MainViewModel GetInstance(FirmwareVersionViewModel Version, ProjectViewModel Project)
        {
            return new MainViewModel(Version, Project, _packageSavingService);
        }
    }
}
