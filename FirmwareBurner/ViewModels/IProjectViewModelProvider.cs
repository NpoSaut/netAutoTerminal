using FirmwareBurner.Annotations;
using FirmwareBurner.ViewModels.Targeting;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public interface IProjectViewModelProvider
    {
        ProjectViewModel GetViewModel(int CellKindId, int CellModificationId);
    }

    [UsedImplicitly]
    public class ProjectViewModelProvider : IProjectViewModelProvider
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFirmwareSetConstructorViewModelProvider _firmwareSetConstructorViewModelProvider;

        public ProjectViewModelProvider(IFirmwareSetConstructorViewModelProvider FirmwareSetConstructorViewModelProvider, IEventAggregator EventAggregator)
        {
            _firmwareSetConstructorViewModelProvider = FirmwareSetConstructorViewModelProvider;
            _eventAggregator = EventAggregator;
        }

        public ProjectViewModel GetViewModel(int CellKindId, int CellModificationId)
        {
            return new ProjectViewModel(
                CellKindId, CellModificationId,
                new BlockDetailsViewModel(),
                _firmwareSetConstructorViewModelProvider.GetViewModel(CellKindId, CellModificationId),
                _eventAggregator);
        }
    }
}
