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
        private readonly IPackageSavingTool _packageSavingTool;
        private readonly ProjectViewModelFactory _projectViewModelFactory;
        private readonly IVariablesProcessor _variablesProcessor;

        public MainViewModelFactory(ProjectViewModelFactory ProjectViewModelFactory, ILaunchParameters LaunchParameters, IPackageSavingTool PackageSavingTool,
                                    IVariablesProcessor VariablesProcessor)
        {
            _projectViewModelFactory = ProjectViewModelFactory;
            _packageSavingTool = PackageSavingTool;
            _variablesProcessor = VariablesProcessor;
            _launchParameters = LaunchParameters;
        }

        public MainViewModel GetInstance()
        {
            return new MainViewModel(GetFirmwareVersionViewModel(),
                                     _projectViewModelFactory.GetInstance(),
                                     _packageSavingTool,
                                     _launchParameters,
                                     _variablesProcessor);
        }

        private FirmwareVersionViewModel GetFirmwareVersionViewModel()
        {
            return new FirmwareVersionViewModel(String.Format("{0}.{1}", _launchParameters.VersionMajor ?? 0, _launchParameters.VersionMinor ?? 0),
                                                _launchParameters.VersionLabel,
                                                _launchParameters.ReleaseDate ?? DateTime.Now);
        }
    }
}
