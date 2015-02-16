using System;
using System.Linq;
using FirmwareBurner.Project;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner
{
    public interface IProjectAssembler
    {
        FirmwareProject GetProject(int ChannelNumber);
    }

    public class ViewModelProjectAssembler : IProjectAssembler
    {
        private readonly IFirmwareProjectFactory _firmwareProjectFactory;
        private readonly ProjectViewModel _projectViewModel;

        public ViewModelProjectAssembler(ProjectViewModel ProjectViewModel, IFirmwareProjectFactory FirmwareProjectFactory)
        {
            _projectViewModel = ProjectViewModel;
            _firmwareProjectFactory = FirmwareProjectFactory;
        }

        public FirmwareProject GetProject(int ChannelNumber)
        {
            return _firmwareProjectFactory.GetProject(_projectViewModel.CellKindId, _projectViewModel.CellModificationId, ChannelNumber,
                                                      _projectViewModel.BlockDetails.SerialNumber,
                                                      _projectViewModel.BlockDetails.AssemblyDate,
                                                      _projectViewModel.FirmwareSetConstructor.Components.Select(
                                                          c => Tuple.Create(c.ModuleIndex, c.SelectedFirmware)).ToList());
        }
    }
}
