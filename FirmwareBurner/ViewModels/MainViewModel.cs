using System;
using System.Linq;
using System.Windows.Input;
using FirmwareBurner.Events;
using FirmwareBurner.Models.Project;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //private readonly IBurnManager _burnManager;
        private readonly IFirmwareProjectFactory _firmwareProjectFactory;
        private readonly IProjectViewModelProvider _projectViewModelProvider;

        public MainViewModel(TargetSelectorViewModel TargetSelector, IEventAggregator EventAggregator, IProjectViewModelProvider ProjectViewModelProvider,
                             IFirmwareProjectFactory FirmwareProjectFactory)
        {
            this.TargetSelector = TargetSelector;
            _projectViewModelProvider = ProjectViewModelProvider;
            _firmwareProjectFactory = FirmwareProjectFactory;

            EventAggregator.GetEvent<TargetSelectedEvent>().Subscribe(OnTargetSelected);

            BurnCommand = new DelegateCommand(Burn);
        }

        public TargetSelectorViewModel TargetSelector { get; private set; }
        public ProjectViewModel Project { get; private set; }
        public ICommand BurnCommand { get; private set; }

        private void OnTargetSelected(TargetSelectedArgs SelectedArgs)
        {
            Project = _projectViewModelProvider.GetViewModel(SelectedArgs.CellKindId, SelectedArgs.ModificationId);
            RaisePropertyChanged("Project");
        }

        private void Burn()
        {
            // TODO: channel number!
            FirmwareProject project = _firmwareProjectFactory.GetProject(Project.CellKindId, Project.CellModificationId, 1, Project.BlockDetails.SerialNumber,
                                                                         Project.BlockDetails.AssemblyDate,
                                                                         Project.FirmwareSetConstructor.Components.Select(
                                                                             c => Tuple.Create(c.ModuleIndex, c.FirmwareSelector.SelectedPackage)).ToList());
            //_burnManager.Burn(project);
        }
    }
}
