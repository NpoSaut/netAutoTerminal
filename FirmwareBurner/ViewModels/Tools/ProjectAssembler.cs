using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.FirmwareProviders;
using FirmwareBurner.Models;
using FirmwareBurner.Models.Project;

namespace FirmwareBurner.ViewModels.Tools
{
    public class ProjectAssembler
    {
        public FirmwareProject GetProject(ProjectViewModel ProjectViewModel, IFirmwaresSet FirmwaresSet)
        {
            TargetInformation target = ComposeTargetInformation(ProjectViewModel);
            IList<ModuleProject> moduleProjects = GetModuleProjects(FirmwaresSet);

            GetModuleProjects(FirmwaresSet);

            return new FirmwareProject(target, moduleProjects);
        }

        private IList<ModuleProject> GetModuleProjects(IFirmwaresSet FirmwaresSet)
        {
            return
                FirmwaresSet.Modules
                            .Select(m =>
                                    new ModuleProject(
                                        new ModuleInformation(),
                                        FirmwaresSet.GetFirmwareInformation(m),
                                        FirmwaresSet.GetComponent(m)))
                            .ToList();
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
