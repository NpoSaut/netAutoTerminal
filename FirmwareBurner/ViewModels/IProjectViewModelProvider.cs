using FirmwareBurner.ViewModels.FirmwareSources;
using FirmwareBurner.ViewModels.Targeting;

namespace FirmwareBurner.ViewModels
{
    public interface IProjectViewModelProvider
    {
        ProjectViewModel GetViewModel();
    }

    public class ProjectViewModelProvider : IProjectViewModelProvider
    {
        private readonly IFirmwareSelectorViewModelProvider _firmwareSelectorViewModelProvider;

        public ProjectViewModelProvider(IFirmwareSelectorViewModelProvider FirmwareSelectorViewModelProvider)
        {
            _firmwareSelectorViewModelProvider = FirmwareSelectorViewModelProvider;
        }

        public ProjectViewModel GetViewModel()
        {
            return new ProjectViewModel(
                new BlockDetailsViewModel(),
                _firmwareSelectorViewModelProvider.GetViewModel(1, 1));
        }
    }
}
