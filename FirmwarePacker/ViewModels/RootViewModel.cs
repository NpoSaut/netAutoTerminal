using System;
using FirmwarePacker.Events;
using FirmwarePacker.LaunchParameters;
using FirmwarePacker.ViewModels.Factories;
using Microsoft.Practices.Prism.Events;

namespace FirmwarePacker.ViewModels
{
    public class RootViewModel : ViewModel
    {
        private readonly ILaunchParameters _launchParameters;
        private readonly ILoadProjectService _loadProjectService;
        private readonly MainViewModelFactory _mainViewModelFactory;
        private ViewModel _actualViewModel;

        public RootViewModel(MainViewModelFactory MainViewModelFactory, SelectProjectViewModelFactory SelectProjectViewModelFactory,
                             IEventAggregator EventAggregator, ILaunchParameters LaunchParameters, ILoadProjectService LoadProjectService)
        {
            _mainViewModelFactory = MainViewModelFactory;
            _launchParameters = LaunchParameters;
            _loadProjectService = LoadProjectService;

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

            return _mainViewModelFactory.GetInstance(GetFirmwareVersionViewModel(),
                                                     _loadProjectService.LoadProject(_launchParameters.ProjectFileName));
        }

        private void ReloadViewModel(ProjectLoadedEvent.Payload Payload)
        {
            ActualViewModel = _mainViewModelFactory.GetInstance(GetFirmwareVersionViewModel(), Payload.Project);
        }

        private FirmwareVersionViewModel GetFirmwareVersionViewModel()
        {
            return new FirmwareVersionViewModel(String.Format("{0}.{1}", _launchParameters.VersionMajor ?? 0, _launchParameters.VersionMinor ?? 0),
                                                _launchParameters.VersionLabel,
                                                _launchParameters.ReleaseDate ?? DateTime.Now);
        }
    }
}
