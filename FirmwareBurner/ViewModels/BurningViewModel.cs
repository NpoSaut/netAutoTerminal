using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Burning;
using FirmwareBurner.Project;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels
{
    public class BurningViewModel : ViewModelBase
    {
        private readonly IBurningService _burningService;
        private readonly IProjectAssembler _projectAssembler;

        public BurningViewModel(IProjectAssembler ProjectAssembler, IBurningService BurningService,
                                ICollection<BurningOptionViewModel> BurningOptions, ICollection<BurningMethodViewModel> BurningMethods)
        {
            this.BurningOptions = BurningOptions;
            this.BurningMethods = BurningMethods;
            _burningService = BurningService;
            _projectAssembler = ProjectAssembler;

            foreach (BurningOptionViewModel burningOption in BurningOptions)
                burningOption.Activated += BurningOptionOnActivated;

            SelectedBurningMethod = BurningMethods.FirstOrDefault();
        }

        public BurningMethodViewModel SelectedBurningMethod { get; set; }
        public ICollection<BurningOptionViewModel> BurningOptions { get; private set; }
        public ICollection<BurningMethodViewModel> BurningMethods { get; private set; }

        private void BurningOptionOnActivated(object Sender, EventArgs EventArgs)
        {
            var activatedOption = (BurningOptionViewModel)Sender;
            FirmwareProject project = _projectAssembler.GetProject(activatedOption.ChannelNumber);
            _burningService.BeginBurn(SelectedBurningMethod.Receipt, project, activatedOption.Progress);
        }
    }
}
