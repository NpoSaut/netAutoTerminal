using FirmwarePacker.Project.Serializers;
using FirmwarePacker.Shared.LaunchParameters;

namespace FirmwarePacker.ViewModels
{
    public class ProjectViewModelFactory
    {
        private readonly ProjectInformationViewModelFactory _informationViewModelFactory;
        private readonly ILaunchParameters _launchParameters;
        private readonly IProjectSerializer _projectSerializer;

        public ProjectViewModelFactory(IProjectSerializer ProjectSerializer, ILaunchParameters LaunchParameters,
                                       ProjectInformationViewModelFactory InformationViewModelFactory)
        {
            _projectSerializer = ProjectSerializer;
            _launchParameters = LaunchParameters;
            _informationViewModelFactory = InformationViewModelFactory;
        }

        public ProjectViewModel GetInstance()
        {
            return new ProjectViewModel(_launchParameters.ProjectFileName, _projectSerializer, _informationViewModelFactory, _launchParameters.RootDirectory);
        }
    }
}
