using System;
using System.Linq;
using FirmwareBurner.Burning;
using FirmwareBurner.Events;
using FirmwareBurner.Project;
using FirmwareBurner.Settings;
using FirmwareBurner.Validation;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public class BurningViewModelController
    {
        private readonly IBurningService _burningService;
        private readonly int _cellKindId;
        private readonly IProjectAssembler _projectAssembler;
        private readonly ISettingsService _settingsService;
        private readonly IValidationContext _validationContext;
        private readonly BurningViewModel _viewModel;

        public BurningViewModelController(BurningViewModel ViewModel, int CellKindId, IEventAggregator EventAggregator, ISettingsService SettingsService,
                                          IValidationContext ValidationContext, IProjectAssembler ProjectAssembler, IBurningService BurningService)
        {
            _viewModel = ViewModel;
            _settingsService = SettingsService;
            _validationContext = ValidationContext;
            _projectAssembler = ProjectAssembler;
            _burningService = BurningService;
            _cellKindId = CellKindId;
            _viewModel.SelectedBurningMethodChanged += ViewModelOnSelectedBurningMethodChanged;

            foreach (BurningOptionViewModel burningOption in _viewModel.BurningOptions)
                burningOption.Activated += BurningOptionOnActivated;

            string preferredBurningMethod = _settingsService.GetPreferredBurningMethod(CellKindId);
            _viewModel.SelectedBurningMethod =
                _viewModel.BurningMethods.FirstOrDefault(m => m.Name == preferredBurningMethod)
                ?? _viewModel.BurningMethods.FirstOrDefault();
            EventAggregator.GetEvent<ProjectChangedEvent>().Subscribe(OnProjectChanged);
        }

        private void ViewModelOnSelectedBurningMethodChanged(object Sender, EventArgs Args)
        {
            _settingsService.SetPreferredBurningMethod(_cellKindId, _viewModel.SelectedBurningMethod.Name);
            _settingsService.Save();
        }

        private void OnProjectChanged(ProjectChangedArgs e)
        {
            foreach (BurningOptionViewModel burningOption in _viewModel.BurningOptions)
                burningOption.ResetOperation();
        }

        private void BurningOptionOnActivated(object Sender, EventArgs EventArgs)
        {
            var activatedOption = (BurningOptionViewModel)Sender;
            if (_validationContext.Check())
            {
                FirmwareProject project = _projectAssembler.GetProject(activatedOption.ChannelNumber);
                activatedOption.ProcessAsyncOperation(
                    _burningService.BeginBurn(_viewModel.SelectedBurningMethod.Receipt, project));
            }
        }
    }
}
