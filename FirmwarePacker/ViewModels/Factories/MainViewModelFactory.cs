using System;
using FirmwarePacker.Enpacking;
using FirmwarePacker.LaunchParameters;
using FirmwarePacking.Annotations;

namespace FirmwarePacker.ViewModels.Factories
{
    [UsedImplicitly]
    public class MainViewModelFactory
    {
        private readonly ILaunchParameters _launchParameters;
        private readonly IPackageSavingService _packageSavingService;
        private readonly ProjectViewModelFactory _projectViewModelFactory;

        public MainViewModelFactory(ProjectViewModelFactory ProjectViewModelFactory, ILaunchParameters LaunchParameters, IPackageSavingTool PackageSavingTool,
                                    IVariablesProcessor VariablesProcessor, IPackageSavingService PackageSavingService)
        {
            _projectViewModelFactory = ProjectViewModelFactory;
            _launchParameters = LaunchParameters;
            _packageSavingService = PackageSavingService;
        }

        public MainViewModel GetInstance()
        {
            return new MainViewModel(GetFirmwareVersionViewModel(), _projectViewModelFactory.GetInstance(), _packageSavingService);
        }

        private FirmwareVersionViewModel GetFirmwareVersionViewModel()
        {
            return new FirmwareVersionViewModel(String.Format("{0}.{1}", _launchParameters.VersionMajor ?? 0, _launchParameters.VersionMinor ?? 0),
                                                _launchParameters.VersionLabel,
                                                _launchParameters.ReleaseDate ?? DateTime.Now);
        }
    }
}
