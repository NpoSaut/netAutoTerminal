using System;
using FirmwareBurner.Models;
using FirmwareBurner.Models.Project;

namespace FirmwareBurner.ViewModels
{
    public class ProjectAssembler
    {
        public FirmwareProject GetProject(ProjectViewModel ProjectViewModel)
        {
            //            return new FirmwareProject(
            //                ComposeTargetInformation(), );
            throw new NotImplementedException();
        }

        private TargetInformation ComposeTargetInformation(ProjectViewModel ProjectViewModel)
        {
            return new TargetInformation(
                ProjectViewModel.TargetSelector.SelectedChannel.Number,
                ProjectViewModel.TargetSelector.SelectedCellKind.Id,
                ProjectViewModel.TargetSelector.SelectedModificationKind.Id,
                ProjectViewModel.BlockDetails.SerialNumber,
                ProjectViewModel.BlockDetails.AssemblyDate);
        }
    }
}
