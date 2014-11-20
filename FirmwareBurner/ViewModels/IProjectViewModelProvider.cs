using FirmwareBurner.Annotations;
using FirmwareBurner.ViewModels.Targeting;

namespace FirmwareBurner.ViewModels
{
    public interface IProjectViewModelProvider
    {
        ProjectViewModel GetViewModel(int CellKindId, int CellModificationId);
    }

    [UsedImplicitly]
    public class ProjectViewModelProvider : IProjectViewModelProvider
    {
        private readonly IFirmwareSetConstructorViewModelProvider _firmwareSetConstructorViewModelProvider;

        public ProjectViewModelProvider(IFirmwareSetConstructorViewModelProvider FirmwareSetConstructorViewModelProvider)
        {
            _firmwareSetConstructorViewModelProvider = FirmwareSetConstructorViewModelProvider;
        }

        public ProjectViewModel GetViewModel(int CellKindId, int CellModificationId)
        {
            return new ProjectViewModel(
                CellKindId, CellModificationId,
                new BlockDetailsViewModel(),
                _firmwareSetConstructorViewModelProvider.GetViewModel(CellKindId, CellModificationId));
        }
    }
}
