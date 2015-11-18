using System;
using System.Linq;
using FirmwarePacker.Events;
using FirmwarePacker.LaunchParameters;
using FirmwarePacker.RecentProjects;
using FirmwarePacker.ViewModels.Factories;
using Microsoft.Practices.Prism.Events;

namespace FirmwarePacker.ViewModels
{
    public class RootViewModel : ViewModel
    {
        private readonly ILaunchParameters _launchParameters;
        private readonly ILoadProjectService _loadProjectService;
        private readonly MainViewModelFactory _mainViewModelFactory;
        private readonly IRecentProjectsService _recentProjectsService;
        private ViewModel _actualViewModel;

        public RootViewModel(MainViewModelFactory MainViewModelFactory, SelectProjectViewModelFactory SelectProjectViewModelFactory,
                             IEventAggregator EventAggregator, ILaunchParameters LaunchParameters, ILoadProjectService LoadProjectService,
                             IRecentProjectsService RecentProjectsService)
        {
            _mainViewModelFactory = MainViewModelFactory;
            _launchParameters = LaunchParameters;
            _loadProjectService = LoadProjectService;
            _recentProjectsService = RecentProjectsService;

            ActualViewModel = GetDefaultViewModel(SelectProjectViewModelFactory);
            EventAggregator.GetEvent<ProjectLoadedEvent>().Subscribe(ReloadViewModel);
        }

        public ViewModel ActualViewModel
        {
            get { return _actualViewModel; }
            private set
            {
                if (Equals(value, _actualViewModel)) return;
                _actualViewModel = value;
                RaisePropertyChanged("ActualViewModel");
            }
        }

        private ViewModel GetDefaultViewModel(SelectProjectViewModelFactory SelectProjectViewModelFactory)
        {
            if (string.IsNullOrWhiteSpace(_launchParameters.ProjectFileName))
                return SelectProjectViewModelFactory.GetViewModel();

            return _mainViewModelFactory.GetInstance(GetFirmwareVersionViewModel(_launchParameters.ProjectFileName,
                                                                                 _launchParameters.VersionMajor,
                                                                                 _launchParameters.VersionMinor,
                                                                                 _launchParameters.VersionLabel,
                                                                                 _launchParameters.ReleaseDate),
                                                     _loadProjectService.LoadProject(_launchParameters.ProjectFileName));
        }

        private void ReloadViewModel(ProjectLoadedEvent.Payload Payload)
        {
            ActualViewModel = _mainViewModelFactory.GetInstance(GetFirmwareVersionViewModel(Payload.Project.FilePath), Payload.Project);
        }

        private FirmwareVersionViewModel GetFirmwareVersionViewModel(string ProjectFilePath, int? Major = null, int? Minor = null, string Label = null,
                                                                     DateTime? ReleaseDate = null)
        {
            Version recentVersion = GetRecentVersion(ProjectFilePath);
            var parametrizedVersion = new Version(Major ?? recentVersion.Major, Minor ?? recentVersion.Minor);
            Version version = parametrizedVersion != recentVersion || Minor.HasValue
                                  ? parametrizedVersion
                                  : new Version(parametrizedVersion.Major, parametrizedVersion.Minor + 1);

            return new FirmwareVersionViewModel(version.ToString(2), Label, ReleaseDate ?? DateTime.Now);
        }

        private Version GetRecentVersion(string FileName)
        {
            RecentProject recent = _recentProjectsService.GetRecentProjects().FirstOrDefault(p => p.FileName == FileName);
            return recent != null
                       ? new Version(recent.MajorVersion, recent.MinorVersion)
                       : new Version(0, 0);
        }
    }
}
