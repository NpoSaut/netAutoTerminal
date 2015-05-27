using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Burning;
using FirmwareBurner.Events;
using FirmwareBurner.Project;
using FirmwareBurner.Settings;
using FirmwareBurner.Validation;
using FirmwareBurner.ViewModels.Bases;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public class BurningViewModel : ViewModelBase
    {
        private readonly IBurningService _burningService;
        private readonly int _cellKindId;
        private readonly IProjectAssembler _projectAssembler;
        private readonly ISettingsService _settingsService;
        private readonly IValidationContext _validationContext;
        private BurningMethodViewModel _selectedBurningMethod;

        public BurningViewModel(int CellKindId, IProjectAssembler ProjectAssembler, IBurningService BurningService,
                                IEventAggregator EventAggregator, ICollection<BurningOptionViewModel> BurningOptions,
                                ICollection<BurningMethodViewModel> BurningMethods, IValidationContext ValidationContext, ISettingsService SettingsService)
        {
            this.BurningOptions = BurningOptions;
            this.BurningMethods = BurningMethods;
            _burningService = BurningService;
            _validationContext = ValidationContext;
            _settingsService = SettingsService;
            _cellKindId = CellKindId;
            _projectAssembler = ProjectAssembler;

            foreach (BurningOptionViewModel burningOption in BurningOptions)
                burningOption.Activated += BurningOptionOnActivated;

            string preferredBurningMethod = _settingsService.GetPreferredBurningMethod(_cellKindId);
            SelectedBurningMethod =
                BurningMethods.FirstOrDefault(m => m.Name == preferredBurningMethod)
                ?? BurningMethods.FirstOrDefault();
            EventAggregator.GetEvent<ProjectChangedEvent>().Subscribe(OnProjectChanged);
        }

        public BurningMethodViewModel SelectedBurningMethod
        {
            get { return _selectedBurningMethod; }
            set
            {
                if (Equals(value, _selectedBurningMethod)) return;
                _selectedBurningMethod = value;
                _settingsService.SetPreferredBurningMethod(_cellKindId, value.Name);
                _settingsService.Save();
                RaisePropertyChanged(() => SelectedBurningMethod);
            }
        }

        public ICollection<BurningOptionViewModel> BurningOptions { get; private set; }
        public ICollection<BurningMethodViewModel> BurningMethods { get; private set; }

        private void OnProjectChanged(ProjectChangedArgs e)
        {
            foreach (BurningOptionViewModel burningOption in BurningOptions)
                burningOption.ResetOperation();
        }

        private void BurningOptionOnActivated(object Sender, EventArgs EventArgs)
        {
            var activatedOption = (BurningOptionViewModel)Sender;
            if (_validationContext.Check())
            {
                FirmwareProject project = _projectAssembler.GetProject(activatedOption.ChannelNumber);
                activatedOption.ProcessAsyncOperation(
                    _burningService.BeginBurn(SelectedBurningMethod.Receipt, project));
            }
        }
    }
}
