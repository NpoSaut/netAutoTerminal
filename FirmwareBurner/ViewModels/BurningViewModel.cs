using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Burning;
using FirmwareBurner.Events;
using FirmwareBurner.Project;
using FirmwareBurner.Validation;
using FirmwareBurner.ViewModels.Bases;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public class BurningViewModel : ViewModelBase
    {
        private readonly IBurningService _burningService;
        private readonly IProjectAssembler _projectAssembler;
        private readonly IValidationContext _validationContext;
        private BurningMethodViewModel _selectedBurningMethod;

        public BurningViewModel(IProjectAssembler ProjectAssembler, IBurningService BurningService, IEventAggregator EventAggregator,
                                ICollection<BurningOptionViewModel> BurningOptions, ICollection<BurningMethodViewModel> BurningMethods,
                                IValidationContext ValidationContext)
        {
            this.BurningOptions = BurningOptions;
            this.BurningMethods = BurningMethods;
            _burningService = BurningService;
            _validationContext = ValidationContext;
            _projectAssembler = ProjectAssembler;

            foreach (BurningOptionViewModel burningOption in BurningOptions)
                burningOption.Activated += BurningOptionOnActivated;

            SelectedBurningMethod = BurningMethods.FirstOrDefault();
            EventAggregator.GetEvent<ProjectChangedEvent>().Subscribe(OnProjectChanged);
        }

        public BurningMethodViewModel SelectedBurningMethod
        {
            get { return _selectedBurningMethod; }
            set
            {
                if (Equals(value, _selectedBurningMethod)) return;
                _selectedBurningMethod = value;
                RaisePropertyChanged(() => SelectedBurningMethod);
            }
        }

        public ICollection<BurningOptionViewModel> BurningOptions { get; private set; }
        public ICollection<BurningMethodViewModel> BurningMethods { get; private set; }

        private void OnProjectChanged(ProjectChangedArgs e)
        {
            foreach (BurningOptionViewModel burningOption in BurningOptions)
                burningOption.Progress.Reset();
        }

        private void BurningOptionOnActivated(object Sender, EventArgs EventArgs)
        {
            if (!_validationContext.Check()) return;
            var activatedOption = (BurningOptionViewModel)Sender;
            FirmwareProject project = _projectAssembler.GetProject(activatedOption.ChannelNumber);
            _burningService.BeginBurn(SelectedBurningMethod.Receipt, project, activatedOption.Progress);
        }
    }
}
