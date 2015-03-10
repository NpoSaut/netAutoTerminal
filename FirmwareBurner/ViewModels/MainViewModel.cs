using FirmwareBurner.Events;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ProjectManagerViewModelFactory _projectManagerViewModelFactory;

        public MainViewModel(TargetSelectorViewModel TargetSelector, TargetPresenterViewModel TargetPresenter, IEventAggregator EventAggregator,
                             FileRequestServiceViewModel FileRequestServiceViewModel, ProjectManagerViewModelFactory ProjectManagerViewModelFactory,
                             IExceptionDialogSource ExceptionDialogSource)
        {
            this.TargetSelector = TargetSelector;
            this.FileRequestServiceViewModel = FileRequestServiceViewModel;
            _projectManagerViewModelFactory = ProjectManagerViewModelFactory;
            this.TargetPresenter = TargetPresenter;
            this.ExceptionDialogSource = ExceptionDialogSource;

            EventAggregator.GetEvent<TargetSelectedEvent>().Subscribe(OnTargetSelected);
        }

        public FileRequestServiceViewModel FileRequestServiceViewModel { get; private set; }

        public IExceptionDialogSource ExceptionDialogSource { get; private set; }
        public TargetSelectorViewModel TargetSelector { get; private set; }
        public TargetPresenterViewModel TargetPresenter { get; private set; }
        public ProjectManagerViewModel ProjectManager { get; private set; }

        private void OnTargetSelected(TargetSelectedArgs SelectedArgs)
        {
            ProjectManager = SelectedArgs.IsUnselected
                                 ? null
                                 : _projectManagerViewModelFactory.GetViewModel(SelectedArgs.CellKindId, SelectedArgs.ModificationId);
            RaisePropertyChanged("ProjectManager");
        }
    }
}
