using System.Collections.ObjectModel;
using FirmwarePacker.LaunchParameters;
using FirmwarePacker.Project;
using FirmwarePacker.Project.Serializers;

namespace FirmwarePacker.ViewModels
{
    public class ProjectViewModelFactory
    {
        private readonly ILaunchParameters _launchParameters;
        private readonly IProjectSerializer _projectSerializer;

        public ProjectViewModelFactory(IProjectSerializer ProjectSerializer, ILaunchParameters LaunchParameters)
        {
            _projectSerializer = ProjectSerializer;
            _launchParameters = LaunchParameters;
        }

        public ProjectViewModel GetInstance()
        {
            PackageProject project = _launchParameters.ProjectFileName != null
                                         ? _projectSerializer.Load(_launchParameters.ProjectFileName)
                                         : new PackageProject(new Collection<ComponentProject>());
            return new ProjectViewModel(project);
        }
    }
}
