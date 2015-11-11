using System;
using FirmwarePacker.LaunchParameters;
using FirmwarePacker.Shared;

namespace FirmwarePacker.ViewModels
{
    public class MainViewModelFactory
    {
        private readonly ILaunchParameters _launchParameters;
        private readonly IPackageSavingTool _packageSavingTool;
        private readonly ProjectViewModelFactory _projectViewModelFactory;

        public MainViewModelFactory(ProjectViewModelFactory ProjectViewModelFactory, ILaunchParameters LaunchParameters, IPackageSavingTool PackageSavingTool)
        {
            _projectViewModelFactory = ProjectViewModelFactory;
            _packageSavingTool = PackageSavingTool;
            _launchParameters = LaunchParameters;
        }

        public MainViewModel GetInstance()
        {
            return new MainViewModel(GetFirmwareVersionViewModel(),
                                     _projectViewModelFactory.GetInstance(),
                                     _packageSavingTool,
                                     _launchParameters);
        }

        private FirmwareVersionViewModel GetFirmwareVersionViewModel()
        {
            return new FirmwareVersionViewModel(String.Format("{0}.{1}", _launchParameters.VersionMajor ?? 0, _launchParameters.VersionMinor ?? 0),
                                                _launchParameters.VersionLabel,
                                                _launchParameters.ReleaseDate ?? DateTime.Now);
        }
    }
}
